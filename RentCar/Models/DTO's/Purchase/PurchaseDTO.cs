using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models.DTO_s.Purchase
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
    }
}
