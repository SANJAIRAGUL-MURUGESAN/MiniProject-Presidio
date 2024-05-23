namespace RailwayReservationApp.Exceptions.SeatExcepions
{
    public class NoSeatsFoundException : Exception
    {
        string msg;
        public NoSeatsFoundException()
        {
            msg = "No Seats Found!";
        }
        public override string Message => msg;
    }
}
