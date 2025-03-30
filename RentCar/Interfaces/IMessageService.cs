using RentCar.Models;
using RentCar.Models.DTO_s.Message;
using System.Security.Claims;

namespace RentCar.Interfaces
{
    public interface IMessageService
    {
        public Task<ServiceResponse<MessageCreateDTO>> CreateAsync(MessageCreateDTO dto, ClaimsPrincipal user);
    }
}
