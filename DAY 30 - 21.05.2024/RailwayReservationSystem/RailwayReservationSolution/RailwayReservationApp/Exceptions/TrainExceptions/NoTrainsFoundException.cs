namespace RailwayReservationApp.Exceptions.TrainExceptions
{
    public class NoTrainsFoundException : Exception
    {
        string msg;
        public NoTrainsFoundException()
        {
            msg = "No Trains Found!";
        }
        public override string Message => msg;
    }
}
