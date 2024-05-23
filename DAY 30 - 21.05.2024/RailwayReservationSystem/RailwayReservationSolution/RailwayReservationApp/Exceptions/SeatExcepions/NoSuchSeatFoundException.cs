namespace RailwayReservationApp.Exceptions.SeatExcepions
{
    public class NoSuchSeatFoundException : Exception
    {
        string msg;
        public NoSuchSeatFoundException()
        {
            msg = "No Such Seat Found!";
        }
        public override string Message => msg;

    }
}
