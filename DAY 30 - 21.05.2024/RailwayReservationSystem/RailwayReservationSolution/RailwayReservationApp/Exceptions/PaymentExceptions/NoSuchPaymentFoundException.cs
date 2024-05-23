namespace RailwayReservationApp.Exceptions.PaymentExceptions
{
    public class NoSuchPaymentFoundException : Exception
    {
        string msg;
        public NoSuchPaymentFoundException()
        {
            msg = "No Such Payment Found!";
        }
        public override string Message => msg;
    }
}
