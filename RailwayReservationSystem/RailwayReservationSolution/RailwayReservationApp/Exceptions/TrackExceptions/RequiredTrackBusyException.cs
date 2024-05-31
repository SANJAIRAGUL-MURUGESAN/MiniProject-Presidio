namespace RailwayReservationApp.Exceptions.TrackExceptions
{
    public class RequiredTrackBusyException : Exception
    {
        string msg;
        public RequiredTrackBusyException(string v)
        {
            msg = "The Required Track is Busy! Please alter your Train Track Admin.";
        }
        public RequiredTrackBusyException()
        {
            msg = "The Required Track is Busy! Please alter your Train Track Admin.";
        }
        public override string Message => msg;
    }
}
