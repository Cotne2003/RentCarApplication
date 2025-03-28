using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public List<Message>? Message { get; set; }

        public List<FavoriteCar>? FavoriteCars { get; set; }
    }
}
