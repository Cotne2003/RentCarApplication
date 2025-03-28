using System.Net;

namespace RentCar.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
