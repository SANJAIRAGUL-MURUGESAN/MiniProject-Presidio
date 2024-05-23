namespace RailwayReservationApp.Exceptions.ReservationExceptions
{
    public class NoSuchReservationFoundException : Exception
    {
        string msg;
        public NoSuchReservationFoundException()
        {
            msg = "No Such Reservation Found!";
        }
        public override string Message => msg;
    }
}
