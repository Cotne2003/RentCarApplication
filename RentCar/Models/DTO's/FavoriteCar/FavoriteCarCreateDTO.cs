using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.DTO_s
{
    public class FavoriteCarCreateDTO
    {
        [Required]
        public int CarId { get; set; }
    }
}
