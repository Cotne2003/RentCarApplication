﻿using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.DTO_s.Car
{
    public class CarCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public required string Brand { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Model { get; set; }

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
        public required string Transmission { get; set; }

        [Required]
        [Range(1, 90)]
        public int FuelCapacity { get; set; }

        [Required]
        [MaxLength(100)]
        public required string City { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public required string OwnerPhoneNumber { get; set; }
    }
}
