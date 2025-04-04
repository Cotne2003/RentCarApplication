﻿using RentCar.Models;
using RentCar.Models.DTO_s.Car;
using System.Security.Claims;

namespace RentCar.Interfaces
{
    public interface ICarService
    {
        Task<ServiceResponse<List<CarDTO>>> GetAllAsync();
        Task<ServiceResponse<List<CarDTO>>> GetPopularAsync();
        Task<ServiceResponse<List<CarDTO>>> GetAllPaginatedAsync(int pageNumber, int pageSize);
        Task<ServiceResponse<List<CarDTO>>> GetAllFilteredAsync(int? capacity, int? startYear, int? endYear, string? city, int pageIndex, int pageSize);
        Task<ServiceResponse<List<CarDTO>>> GetAllByPhoneAsync(string phoneNumber);
        Task<ServiceResponse<int>> CreateAsync(CarCreateDTO dto, ClaimsPrincipal user);
        Task<ServiceResponse<List<string>>> GetAllCitiesAsync();
        Task<ServiceResponse<CarDTO>> GetByIdAsync(int id);
    }
}
