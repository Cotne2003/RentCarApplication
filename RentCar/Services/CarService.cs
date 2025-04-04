﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Models.DTO_s.Car;
using RentCar.Models.Entities;
using System.Net;
using System.Security.Claims;

namespace RentCar.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> CreateAsync(CarCreateDTO dto, ClaimsPrincipal user)
        {
            if (dto == null)
            {
                return new ServiceResponse<int>
                {
                    Message = "Invalid car data",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            if (!int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return new ServiceResponse<int>
                {
                    Message = "Not authorized",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            string userEmail = user.FindFirst(ClaimTypes.Email)?.Value;
            string userName = user.Identity.Name;

            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userName))
            {
                return new ServiceResponse<int>
                {
                    Message = "User authentication required",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            Car mappedCar = _mapper.Map<Car>(dto);

            mappedCar.CreatedByEmail = userEmail;
            mappedCar.CreatedBy = userName;
            mappedCar.UserId = userId;

            await _context.cars.AddAsync(mappedCar);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Data = mappedCar.Id,
                Message = "Car added successfully",
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<ServiceResponse<List<CarDTO>>> GetAllAsync()
        {
            List<CarDTO> cars = await _context.cars.Include(x => x.User).Include(x => x.Purchase).ProjectTo<CarDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return new ServiceResponse<List<CarDTO>> { Data = cars, Message = "Cars returned successfully", StatusCode = HttpStatusCode.OK };
        }

        public async Task<ServiceResponse<List<CarDTO>>> GetAllByPhoneAsync(string phoneNumber)
        {
            List<CarDTO> cars = await _context.cars.Where(x => x.OwnerPhoneNumber == phoneNumber).Select(x => _mapper.Map<CarDTO>(x)).ToListAsync();

            return new ServiceResponse<List<CarDTO>>
            {
                Data = cars,
                Message = cars.Count > 0 ? "Cars returned successfully" : "No cars found for this phone number",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ServiceResponse<List<string>>> GetAllCitiesAsync()
        {
            List<string> cities = await _context.cars
                .Select(x => x.City.ToLower())
                .Distinct()
                .ToListAsync();
            return new ServiceResponse<List<string>>
            {
                Data = cities,
                Message = "Cities returned successfully",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ServiceResponse<List<CarDTO>>> GetAllFilteredAsync(int? capacity, int? startYear, int? endYear, string? city, int pageIndex, int pageSize)
        {
            if (startYear.HasValue && endYear.HasValue && endYear < startYear)
            {
                return new ServiceResponse<List<CarDTO>>
                {
                    Message = "Start year must be less than or equal to end year",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var query = _context.cars.AsQueryable();

            if (capacity.HasValue && capacity > 0)
                query = query.Where(x => x.Capacity == capacity);

            if (startYear.HasValue)
                query = query.Where(x => x.Year >= startYear);

            if (endYear.HasValue)
                query = query.Where(x => x.Year <= endYear);

            if (!string.IsNullOrEmpty(city))
                query = query.Where(x => x.City.ToLower() == city.ToLower());

            int totalCars = await query.CountAsync();

            List<CarDTO> cars = await query
                .OrderBy(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CarDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new ServiceResponse<List<CarDTO>>
            {
                Data = cars,
                Message = cars.Count > 0 ? "Filtered cars returned successfully" : "No cars found for the given filters",
                StatusCode = HttpStatusCode.OK,
            };
        }

        public async Task<ServiceResponse<List<CarDTO>>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            int totalCars = await _context.cars.CountAsync();

            List<CarDTO> cars = await _context.cars
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CarDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new ServiceResponse<List<CarDTO>>
            {
                Data = cars,
                Message = cars.Count > 0 ? "Paginated cars returned successfully" : "No cars found on this page",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ServiceResponse<CarDTO>> GetByIdAsync(int id)
        {
            var contextCar = await _context.cars.FirstOrDefaultAsync(x => x.Id == id);

            if (contextCar is null)
            {
                return new ServiceResponse<CarDTO>
                {
                    Message = "Car not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            CarDTO car = _mapper.Map<CarDTO>(contextCar);

            return new ServiceResponse<CarDTO>
            {
                Data = car,
                Message = "Car returned successfully",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ServiceResponse<List<CarDTO>>> GetPopularAsync()
        {
            List<CarDTO> cars = await _context.cars
                .Include(x => x.User)
                .Include(x => x.Purchase)
                .Where(x => x.Purchase == null)
                .ProjectTo<CarDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new ServiceResponse<List<CarDTO>>
            {
                Data = cars,
                Message = "Cars returned successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
