using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RentCar.Models.DTO_s.Car;

namespace RentCar.Models.DTO_s.FavoriteCar
{
    public class FavoriteCarDTO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public CarDTO Car { get; set; }
        public int UserId { get; set; }
    }
}
