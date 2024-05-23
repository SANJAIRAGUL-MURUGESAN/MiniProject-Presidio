namespace RailwayReservationApp.Exceptions.ReservationExceptions
{
    public class NoReservationsFoundException : Exception
    {
        string msg;
        public NoReservationsFoundException()
        {
            msg = "No Reservations Found!";
        }
        public override string Message => msg;
    }
}
