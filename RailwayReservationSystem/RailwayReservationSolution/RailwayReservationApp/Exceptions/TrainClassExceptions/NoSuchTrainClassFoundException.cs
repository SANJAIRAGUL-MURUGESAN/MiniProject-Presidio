namespace RailwayReservationApp.Exceptions.TrainClassExceptions
{
    public class NoSuchTrainClassFoundException : Exception
    {
        string msg;
        public NoSuchTrainClassFoundException()
        {
            msg = "No Such Train Class Found!";
        }
        public override string Message => msg;
    }
}
