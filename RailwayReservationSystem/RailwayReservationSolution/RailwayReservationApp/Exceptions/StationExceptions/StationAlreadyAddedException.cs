namespace RailwayReservationApp.Exceptions.StationExceptions
{
    public class StationAlreadyAddedException : Exception
    {
        string msg;
        public StationAlreadyAddedException()
        {
            msg = "This station is already added in the particular state! Try again with a different station name";
        }
        public override string Message => msg;
    }
}
