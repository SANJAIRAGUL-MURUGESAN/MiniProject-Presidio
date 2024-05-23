namespace RailwayReservationApp.Exceptions.UserExceptions
{
    public class NoUserFoundException : Exception
    {
        string msg;
        public NoUserFoundException()
        {
            msg = "No User Found!";
        }
        public override string Message => msg;
    }
}
