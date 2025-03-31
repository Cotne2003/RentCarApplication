using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Interfaces;
using RentCar.Models.DTO_s.Purchase;
using System.Net;

namespace RentCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(PurchaseCreateDTO dto)
        {
            var response = await _purchaseService.CreatingAsync(dto, User);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                BadRequest(response);
            }

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return Conflict(response);
            }

            return Ok(response);
        }
    }
}
