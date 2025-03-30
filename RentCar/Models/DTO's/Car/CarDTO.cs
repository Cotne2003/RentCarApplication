using RentCar.Models.Entities;

namespace RentCar.Models.DTO_s.Car
{
    public class CarDTO
    {
        public int Id { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int Year { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public decimal Price { get; set; }
        public int Multiplier { get; set; }
        public int Capacity { get; set; }
        public required string Transmission { get; set; }
        public required string CreatedBy { get; set; }
        public required string CreatedByEmail { get; set; }
        public required int FuelCapacity { get; set; }
        public required string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public required string OwnerPhoneNumber { get; set; }

    }
}
