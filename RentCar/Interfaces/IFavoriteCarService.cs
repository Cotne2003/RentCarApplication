using RentCar.Models;
using RentCar.Models.DTO_s;
using System.Security.Claims;

namespace RentCar.Interfaces
{
    public interface IFavoriteCarService
    {
        Task<ServiceResponse<int>> CreateAsync(FavoriteCarCreateDTO dto, ClaimsPrincipal user);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
