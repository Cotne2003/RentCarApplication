namespace RentCar.Models.DTO_s.Auth
{
    public class JWTOptionsDTO
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
    }
}
