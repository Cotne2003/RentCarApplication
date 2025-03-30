using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.DTO_s.User
{
    public class UserRegisterDTO
    {
        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public required string Password { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }
    }
}
