﻿namespace RailwayReservationApp.Models.UserDTOs
{
    public class UserRegisterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public bool Disability { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
