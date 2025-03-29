using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Models.DTO_s.User;
using System.Net;

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
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            var response = await _authorizationService.LoginAsync(dto);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized(response);
            
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO dto)
        {
            var response = await _authorizationService.RegisterAsync(dto);

            if (response.StatusCode == HttpStatusCode.Conflict)
                return Conflict(response);

            return CreatedAtAction(nameof(Register), new { id = response.Data }, response);
        }
    }
}
