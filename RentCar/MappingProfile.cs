using AutoMapper;
using RentCar.Models.DTO_s.Car;
using RentCar.Models.DTO_s.Message;
using RentCar.Models.DTO_s.Purchase;
using RentCar.Models.DTO_s.User;
using RentCar.Models.Entities;

namespace RentCar
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarCreateDTO>().ReverseMap();
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Message, MessageDTO>().ReverseMap();
            CreateMap<Message, MessageCreateDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseCreateDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
        }
    }
}
