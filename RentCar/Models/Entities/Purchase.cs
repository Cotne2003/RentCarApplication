using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models.Entities
{
    public class Purchase
    {
        public int Id { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
