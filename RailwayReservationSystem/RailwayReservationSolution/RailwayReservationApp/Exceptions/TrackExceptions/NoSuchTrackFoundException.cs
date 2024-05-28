namespace RailwayReservationApp.Exceptions.TrackExceptions
{
    public class NoSuchTrackFoundException : Exception
    {
        string msg;
        public NoSuchTrackFoundException()
        {
            msg = "No Such Train Track Found!";
        }
        public override string Message => msg;
    }
}
