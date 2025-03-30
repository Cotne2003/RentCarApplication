using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Models.DTO_s.User;
using System.Net;
using System.Security.Claims;

namespace RentCar.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserDTO>> GetUserAsync(ClaimsPrincipal user)
        {
            if (!int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return new ServiceResponse<UserDTO>
                {
                    Message = "Not authorized",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            var userInf = await _context.users.Include(x => x.Messages).Include(x => x.Cars).FirstOrDefaultAsync(x => x.Id == userId);

            var mappedUser = _mapper.Map<UserDTO>(userInf);

            return new ServiceResponse<UserDTO>
            {
                Data = mappedUser,
                Message = "User information returned successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
