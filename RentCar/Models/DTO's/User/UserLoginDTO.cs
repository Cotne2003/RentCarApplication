using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.DTO_s.User
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public required string Password { get; set; }
        public bool StaySignedIn { get; set; } = false;
    }
}
