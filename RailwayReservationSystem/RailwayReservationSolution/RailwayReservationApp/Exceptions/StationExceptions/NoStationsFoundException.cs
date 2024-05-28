namespace RailwayReservationApp.Exceptions.StationExceptions
{
    public class NoStationsFoundException : Exception
    {
        string msg;
        public NoStationsFoundException()
        {
            msg = "No Stations Found!";
        }
        public override string Message => msg;
    }
}
