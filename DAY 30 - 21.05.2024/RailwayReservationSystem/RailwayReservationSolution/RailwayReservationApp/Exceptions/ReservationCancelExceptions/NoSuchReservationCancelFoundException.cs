namespace RailwayReservationApp.Exceptions.ReservationCancelExceptions
{
    public class NoSuchReservationCancelFoundException : Exception
    {
        string msg;
        public NoSuchReservationCancelFoundException()
        {
            msg = "No Such Reservation Cancel Found!";
        }
        public override string Message => msg;
    }
}
