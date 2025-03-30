using AutoMapper;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Models.DTO_s.Message;
using RentCar.Models.Entities;
using System.Net;
using System.Security.Claims;

namespace RentCar.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MessageService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<MessageCreateDTO>> CreateAsync(MessageCreateDTO dto, ClaimsPrincipal user)
        {
            if (dto == null)
            {
                return new ServiceResponse<MessageCreateDTO>
                {
                    Message = "Invalid data",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var senderUserName = user.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(senderUserName))
            {
                return new ServiceResponse<MessageCreateDTO>
                {
                    Message = "Sender username not found",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var receiverUser = await _context.users.FindAsync(dto.UserId);

            if (receiverUser.Id == dto.UserId)
            {
                return new ServiceResponse<MessageCreateDTO>
                {
                    Message = "You Can not send message yourself",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            if (receiverUser is null)
            {
                return new ServiceResponse<MessageCreateDTO>
                {
                    Message = "User not found",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            Message message = new Message
            {
                MessageText = dto.MessageText,
                SenderUserName = senderUserName,
                UserId = dto.UserId
            };


            receiverUser.Messages ??= new List<Message>();
            receiverUser.Messages.Add(message);

            await _context.messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return new ServiceResponse<MessageCreateDTO>
            {
                Data = dto,
                Message = "Message sent successfully",
                StatusCode = HttpStatusCode.Created
            };

        }
    }
}
