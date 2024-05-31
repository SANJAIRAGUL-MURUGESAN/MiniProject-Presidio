using System.Diagnostics.CodeAnalysis;

namespace RailwayReservationApp.Exceptions.AdminExceptions
{
    [ExcludeFromCodeCoverage]
    public class NoAdminsFoundException : Exception
    {
        string msg;
        public NoAdminsFoundException()
        {
            msg = "No Admins Found!";
        }
        public override string Message => msg;
    }
}
