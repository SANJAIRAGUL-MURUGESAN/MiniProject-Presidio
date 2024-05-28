namespace RailwayReservationApp.Exceptions.TrackExceptions
{
    public class TrackAlreadyAddedException : Exception
    {

        string msg;
        public TrackAlreadyAddedException()
        {
            msg = "This Particular Track Number is already added to the station!";
        }
        public override string Message => msg;
    }
}
