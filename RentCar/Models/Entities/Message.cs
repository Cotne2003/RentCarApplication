using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models.Entities
{
    public class Message : BaseClass
    {
        public int Id { get; set; }
        public required string MessageText { get; set; }
        public required string SenderUserName { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
