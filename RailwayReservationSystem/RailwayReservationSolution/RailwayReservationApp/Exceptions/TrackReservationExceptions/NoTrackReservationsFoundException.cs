namespace RailwayReservationApp.Exceptions.TrackReservationExceptions
{
    public class NoTrackReservationsFoundException : Exception
    {
        string msg;
        public NoTrackReservationsFoundException()
        {
            msg = "No Track Reservations Found!";
        }
        public override string Message => msg;
    }
}
