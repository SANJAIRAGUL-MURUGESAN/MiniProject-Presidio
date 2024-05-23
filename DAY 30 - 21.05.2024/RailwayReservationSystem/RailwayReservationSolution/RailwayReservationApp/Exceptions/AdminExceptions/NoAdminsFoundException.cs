namespace RailwayReservationApp.Exceptions.AdminExceptions
{
    public class NoAdminsFoundException : Exception
    {
        string msg;
        public NoAdminsFoundException()
        {
            msg = "No Admins Found!";
        }
        public override string Message => msg;
    }
}
