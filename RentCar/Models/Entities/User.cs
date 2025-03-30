using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
