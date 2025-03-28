using RentCar.Models;
using RentCar.Models.DTO_s.User;

namespace RentCar.Interfaces
{
    public interface IAuthorization
    {
        public Task<ServiceResponse<int>> RegisterAsync(UserRegisterDTO dto);
        public Task<ServiceResponse<string>> LoginAsync(UserLoginDTO dto);
    }
}
