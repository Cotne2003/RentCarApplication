using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models.Entities
{
    public class FavoriteCar
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Car")]
        public int CarId { get; set; }

        [Required]
        public Car Car { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }
    }
}
