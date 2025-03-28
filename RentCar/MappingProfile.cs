using AutoMapper;
using RentCar.Models.DTO_s.Car;
using RentCar.Models.Entities;

namespace RentCar
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarCreateDTO>().ReverseMap();
            CreateMap<Car, CarDTO>().ReverseMap();
        }
    }
}
