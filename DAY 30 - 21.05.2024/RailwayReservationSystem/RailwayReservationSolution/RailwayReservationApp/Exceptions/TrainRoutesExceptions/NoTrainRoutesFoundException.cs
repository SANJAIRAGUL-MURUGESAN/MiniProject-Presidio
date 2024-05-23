namespace RailwayReservationApp.Exceptions.TrainRoutesExceptions
{
    public class NoTrainRoutesFoundException : Exception
    {
        string msg;
        public NoTrainRoutesFoundException()
        {
            msg = "No Train Routes Found!";
        }
        public override string Message => msg;
    }
}
