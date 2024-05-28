namespace RailwayReservationApp.Exceptions.TrackReservationExceptions
{
    public class NoSuchTrackReservationFoundException : Exception
    {
        string msg;
        public NoSuchTrackReservationFoundException()
        {
            msg = "No Such Track Reservation Found!";
        }
        public override string Message => msg;
    }
}
