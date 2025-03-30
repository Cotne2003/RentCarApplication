using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.DTO_s.Message
{
    public class MessageCreateDTO
    {
        [Required]
        [MaxLength(1000)]
        public required string MessageText { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
