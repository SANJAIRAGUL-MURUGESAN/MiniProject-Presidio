namespace RailwayReservationApp.Exceptions.StationExceptions
{
    public class NoSuchStationFoundException : Exception
    {
        string msg;
        public NoSuchStationFoundException()
        {
            msg = "No Such Station Found!";
        }
        public override string Message => msg;
    }
}
