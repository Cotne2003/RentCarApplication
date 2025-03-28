using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models.Entities
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string MessageText { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
