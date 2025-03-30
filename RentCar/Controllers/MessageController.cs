using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Interfaces;
using RentCar.Models.DTO_s.Message;
using System.Net;

namespace RentCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(MessageCreateDTO dto)
        {
            var response = await _messageService.CreateAsync(dto, User);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Created("", response);
        }
    }
}
