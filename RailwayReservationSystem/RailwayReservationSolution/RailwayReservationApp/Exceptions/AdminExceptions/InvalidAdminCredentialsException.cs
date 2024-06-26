﻿using System.Diagnostics.CodeAnalysis;

namespace RailwayReservationApp.Exceptions.AdminExceptions
{
    public class InvalidAdminCredentialsException : Exception
    {
        string msg;
        public InvalidAdminCredentialsException()
        {
            msg = "Invalid Username or Password";
        }
        public override string Message => msg;
    }
}
