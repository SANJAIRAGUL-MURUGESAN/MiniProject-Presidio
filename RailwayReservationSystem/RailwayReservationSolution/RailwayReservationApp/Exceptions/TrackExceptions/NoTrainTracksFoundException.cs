namespace RailwayReservationApp.Exceptions.TrackExceptions
{
    public class NoTrainTracksFoundException : Exception
    {
        string msg;
        public NoTrainTracksFoundException()
        {
            msg = "No Tracks Found!";
        }
        public override string Message => msg;
    }
}
