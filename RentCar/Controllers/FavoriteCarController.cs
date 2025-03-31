using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Interfaces;
using RentCar.Models.DTO_s;
using System.Net;

namespace RentCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteCarController : ControllerBase
    {
        private readonly IFavoriteCarService _favoriteCarService;

        public FavoriteCarController(IFavoriteCarService favoriteCarService)
        {
            _favoriteCarService = favoriteCarService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(FavoriteCarCreateDTO dto)
        {
            var response = await _favoriteCarService.CreateAsync(dto, User);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized(response);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }

            return Created("", response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _favoriteCarService.DeleteAsync(id);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return NoContent();
        }
    }
}
