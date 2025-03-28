namespace RentCar.Models
{
    public class BaseClass
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; } = null;
    }
}
