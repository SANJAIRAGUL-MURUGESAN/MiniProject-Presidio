namespace RailwayReservationApp.Exceptions.UserExceptions
{
    public class NoBookedTrainsAvailableException :Exception
    {
        string msg;
        public NoBookedTrainsAvailableException()
        {
            msg = "Hey User, No Booked Trains Available!";
        }
        public override string Message => msg;
    }
}
