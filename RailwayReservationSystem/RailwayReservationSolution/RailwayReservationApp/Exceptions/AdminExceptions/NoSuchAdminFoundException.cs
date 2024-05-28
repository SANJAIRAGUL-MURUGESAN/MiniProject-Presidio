namespace RailwayReservationApp.Exceptions.AdminExceptions
{
    public class NoSuchAdminFoundException :Exception
    {
        string msg;
        public NoSuchAdminFoundException()
        {
            msg = "No Such Admin Found!";
        }
        public override string Message => msg;
    }
}
