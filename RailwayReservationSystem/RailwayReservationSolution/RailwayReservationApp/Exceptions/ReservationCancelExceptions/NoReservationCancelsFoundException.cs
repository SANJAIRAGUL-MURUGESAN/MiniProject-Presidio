using System.Diagnostics.CodeAnalysis;

namespace RailwayReservationApp.Exceptions.ReservationCancelExceptions
{
    public class NoReservationCancelsFoundException : Exception
    {
        string msg;
        public NoReservationCancelsFoundException()
        {
            msg = "No Reservation Cancels Found!";
        }
        public override string Message => msg;
    }
}
