namespace RailwayReservationApp.Exceptions.PaymentExceptions
{
    public class NoPaymentsFoundException : Exception
    {
        string msg;
        public NoPaymentsFoundException()
        {
            msg = "No Payments Found!";
        }
        public override string Message => msg;
    }
}
