﻿namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AdminLoginReturnDTO
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
