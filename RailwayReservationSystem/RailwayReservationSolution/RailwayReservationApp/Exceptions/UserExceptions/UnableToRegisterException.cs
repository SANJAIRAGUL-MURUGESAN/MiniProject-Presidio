namespace RailwayReservationApp.Exceptions.UserExceptions
{
    public class UnableToRegisterException : Exception
    {
        string msg;
        public UnableToRegisterException()
        {
            msg = "Not able to register at this moment! Please Try again after sometime";
        }
        public override string Message => msg;
    }
}
