namespace RailwayReservationApp.Exceptions.TrainExceptions
{
    public class NoSuchTrainFoundException : Exception
    {
        string msg;
        public NoSuchTrainFoundException()
        {
            msg = "No Such Train Found!";
        }
        public override string Message => msg;
    }
}
