using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.DTO_s.User
{
    public class UserLoginDTO
    {
        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public required string Password { get; set; }
        public bool StaySignedIn { get; set; } = false;
    }
}
