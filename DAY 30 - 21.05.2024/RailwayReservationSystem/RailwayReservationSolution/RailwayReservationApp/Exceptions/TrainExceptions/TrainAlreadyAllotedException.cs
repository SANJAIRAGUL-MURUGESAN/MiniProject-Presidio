namespace RailwayReservationApp.Exceptions.TrainExceptions
{
    public class TrainAlreadyAllotedException : Exception
    {
        string msg;
        public TrainAlreadyAllotedException()
        {
            msg = "Hey Admin! This Train is Already Alloted and status is Inline! Please, Try to add another availble Trains.";
        }
        public override string Message => msg;
    }
}
