using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Interfaces;
using System.Net;

namespace RentCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserAsync()
        {
            var response = await _userService.GetUserAsync(User);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Unauthorized(response);
            }

            return Ok(response);
        }
    }
}
