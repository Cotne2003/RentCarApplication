using RentCar.Models;
using RentCar.Models.DTO_s.User;

namespace RentCar.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<ServiceResponse<int>> RegisterAsync(UserRegisterDTO dto);
        public Task<ServiceResponse<string>> LoginAsync(UserLoginDTO dto);
    }
}
