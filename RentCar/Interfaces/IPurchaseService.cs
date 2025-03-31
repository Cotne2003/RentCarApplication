using RentCar.Models;
using RentCar.Models.DTO_s.Purchase;
using System.Security.Claims;

namespace RentCar.Interfaces
{
    public interface IPurchaseService
    {
        public Task<ServiceResponse<int>> CreatingAsync(PurchaseCreateDTO dto, ClaimsPrincipal User);
    }
}
