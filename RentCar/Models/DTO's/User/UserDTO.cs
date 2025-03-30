﻿using RentCar.Models.DTO_s.Car;
using RentCar.Models.DTO_s.Message;
using RentCar.Models.Entities;

namespace RentCar.Models.DTO_s.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<MessageDTO> Messages { get; set; }
        public List<CarDTO> Cars { get; set; }
    }
}
