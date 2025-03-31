using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models.DTO_s.Purchase
{
    public class PurchaseCreateDTO
    {
        [Required]
        public required int CarId { get; set; }
    }
}
