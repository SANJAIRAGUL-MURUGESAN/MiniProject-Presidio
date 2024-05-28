namespace RailwayReservationApp.Exceptions.SeatExcepions
{
    public class SeatAlreadyReservedException : Exception
    {
        string msg;
        public SeatAlreadyReservedException()
        {
            msg = "Seat Already Reserved User! Recheck and book available seats";
        }
        public override string Message => msg;
    }
}
