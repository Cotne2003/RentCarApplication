using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Models.DTO_s.Message;
using RentCar.Models.DTO_s.Purchase;
using RentCar.Models.Entities;
using System.Net;
using System.Security.Claims;

namespace RentCar.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PurchaseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> CreatingAsync(PurchaseCreateDTO dto, ClaimsPrincipal User)
        {
            var car = await _context.cars
                .Include(x => x.Purchase)
                .FirstOrDefaultAsync(x => dto.CarId == x.Id);

            if (car is null)
            {
                return new ServiceResponse<int>
                {
                    Message = "Cannot find the car",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            if (car.Purchase != null)
            {
                return new ServiceResponse<int>
                {
                    Message = "Car is already purchased",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return new ServiceResponse<int>
                {
                    Message = "User not found",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var purchase = _mapper.Map<Purchase>(dto);

            purchase.UserId = userId;

            await _context.purchases.AddAsync(purchase);

            var senderUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(senderUserName))
            {
                return new ServiceResponse<int>
                {
                    Message = "Sender username not found",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var MessageReceiverUser = await _context.users.Where(x => x.Email == car.CreatedByEmail).FirstOrDefaultAsync();
            if (MessageReceiverUser == null)
            {
                return new ServiceResponse<int>
                {
                    Message = "Receiver user not found to send message",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var messageDTO = new MessageCreateDTO
            {
                MessageText = $"{senderUserName} purchase your {car.Model}",
                UserId = MessageReceiverUser.Id,
            };

            var message = _mapper.Map<Message>(messageDTO);

            message.SenderUserName = senderUserName;

            await _context.messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Data = purchase.Id,
                Message = "Car is purchased successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
