using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Models.DTO_s;
using RentCar.Models.Entities;
using System.Net;
using System.Security.Claims;

namespace RentCar.Services
{
    public class FavoriteCarService : IFavoriteCarService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FavoriteCarService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<int>> CreateAsync(FavoriteCarCreateDTO dto, ClaimsPrincipal user)
        {
            if (!int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                return new ServiceResponse<int>
                {
                    Message = "User not authorized",
                    StatusCode = HttpStatusCode.Unauthorized
                };

            var car = await _context.cars.FindAsync(dto.CarId);
            if (car is null)
            {
                return new ServiceResponse<int>
                {
                    Message = "No car found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var favoriteCar = _mapper.Map<FavoriteCar>(dto);
            favoriteCar.UserId = userId;

            await _context.favoriteCars.AddAsync(favoriteCar);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Data = favoriteCar.Id,
                Message = "Car added in favorite successfully",
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var favoriteCarToDelete = await _context.favoriteCars.FindAsync(id);

            if (favoriteCarToDelete == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "No car found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            _context.favoriteCars.Remove(favoriteCarToDelete);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Car deleted successfully",
                StatusCode = HttpStatusCode.NoContent
            };
        }
    }
}
