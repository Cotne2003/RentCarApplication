using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Models.DTO_s.Car;
using System.Net;
using System.Security.Claims;

namespace RentCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var rsponse = await _carService.GetAllAsync();

            return Ok(rsponse);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(CarCreateDTO dto)
        {
            var response = await _carService.CreateAsync(dto, User);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized(response);

            return Created("", response);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            var response = await _carService.GetAllPaginatedAsync(pageNumber, pageSize);

            return Ok(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAllFilteredCars(int? capacity, int? startYear, int? endYear, string? city, int pageIndex, int pageSize)
        {
            var response = await _carService.GetAllFilteredAsync(capacity, startYear, endYear, city, pageIndex, pageSize);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("byPhone")]
        public async Task<IActionResult> GetAllByPhoneAsync(string phoneNumber)
        {
            var response = await _carService.GetAllByPhoneAsync(phoneNumber);

            return Ok(response);
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetAllCitiesAsync()
        {
            var response = await _carService.GetAllCitiesAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _carService.GetByIdAsync(id);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }
    }
}
