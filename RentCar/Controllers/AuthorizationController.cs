using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Models.DTO_s.User;

namespace RentCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorization _authorizationService;

        public AuthorizationController(IAuthorization authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("Login")]
        public async Task<ServiceResponse<string>> Login(UserLoginDTO dto)
        {
            return await _authorizationService.LoginAsync(dto);
        }

        [HttpPost("Register")]
        public async Task<ServiceResponse<int>> Register(UserRegisterDTO dto)
        {
            return await _authorizationService.RegisterAsync(dto);
        }
    }
}
