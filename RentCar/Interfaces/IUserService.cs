using RentCar.Models;
using RentCar.Models.DTO_s.User;
using System.Security.Claims;

namespace RentCar.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<UserDTO>> GetUserAsync(ClaimsPrincipal user);
    }
}
