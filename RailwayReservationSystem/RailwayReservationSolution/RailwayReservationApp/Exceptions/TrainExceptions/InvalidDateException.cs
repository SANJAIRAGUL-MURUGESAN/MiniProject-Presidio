namespace RailwayReservationApp.Exceptions.TrainExceptions
{
    public class InvalidDateException : Exception
    {
        string msg;
        public InvalidDateException()
        {
            msg = "Invalid Date Admin!";
        }
        public override string Message => msg;
    }
}
