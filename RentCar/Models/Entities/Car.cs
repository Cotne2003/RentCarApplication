using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models.Entities
{
    public class Car : BaseClass
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string Transmission {  get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByEmail {  get; set; }
        public int FuelCapacity { get; set; }
        public string City { get; set; }
        public string OwnerPhoneNumber { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public Purchase? Purchase { get; set; }
        public FavoriteCar? FavoriteCar { get; set; }
    }
}
