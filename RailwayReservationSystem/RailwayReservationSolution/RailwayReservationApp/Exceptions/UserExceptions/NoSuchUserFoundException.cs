namespace RailwayReservationApp.Exceptions.UserExceptions
{
    public class NoSuchUserFoundException : Exception
    {
        string msg;
        public NoSuchUserFoundException()
        {
            msg = "No Such User Found!";
        }
        public override string Message => msg;
    }
}
