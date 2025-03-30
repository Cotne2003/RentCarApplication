using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.DTO_s.Message
{
    public class MessageDTO
    {
        public required string MessageText { get; set; }
        public required string SenderUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
