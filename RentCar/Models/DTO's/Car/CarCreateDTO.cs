using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.DTO_s.Car
{
    public class CarCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Required]
        [Range(1950, 2100)]
        public int Year { get; set; }

        [Url]
        public string? ImageUrl1 { get; set; }

        [Url]
        public string? ImageUrl2 { get; set; }

        [Url]
        public string? ImageUrl3 { get; set; }

        [Required]
        [Range(1, 1_000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 20)]
        public int Capacity { get; set; }

        [Required]
        [MaxLength(50)]
        public string Transmission { get; set; }

        [Required]
        [MaxLength(100)]
        public string CreatedBy { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string CreatedByEmail { get; set; }

        [Required]
        [Range(1, 100)]
        public int FuelCapacity { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public string OwnerPhoneNumber { get; set; }
    }
}
