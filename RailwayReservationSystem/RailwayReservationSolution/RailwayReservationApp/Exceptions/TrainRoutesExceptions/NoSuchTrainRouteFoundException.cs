namespace RailwayReservationApp.Exceptions.TrainRoutesExceptions
{
    public class NoSuchTrainRouteFoundException : Exception
    {
        string msg;
        public NoSuchTrainRouteFoundException()
        {
            msg = "No Such Train Route Found!";
        }
        public override string Message => msg;
    }
}
