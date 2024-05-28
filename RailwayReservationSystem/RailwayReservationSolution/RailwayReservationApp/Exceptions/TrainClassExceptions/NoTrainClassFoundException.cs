namespace RailwayReservationApp.Exceptions.TrainClassExceptions
{
    public class NoTrainClassFoundException : Exception
    {
        string msg;
        public NoTrainClassFoundException()
        {
            msg = "No Train Class Found!";
        }
        public override string Message => msg;
    }
}
