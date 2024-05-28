namespace RailwayReservationApp.Exceptions.TrainExceptions
{
    public class NoTrainsAvailableforyourSearchException : Exception
    {
        string msg;
        public NoTrainsAvailableforyourSearchException()
        {
            msg = "Hey User, There is no Trains available for your search!";
        }
        public override string Message => msg;
    }
}
