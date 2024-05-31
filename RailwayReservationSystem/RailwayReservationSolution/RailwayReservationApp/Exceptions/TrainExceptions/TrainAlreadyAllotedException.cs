namespace RailwayReservationApp.Exceptions.TrainExceptions
{
    public class TrainAlreadyAllotedException : Exception
    {
        string msg;
        public TrainAlreadyAllotedException(string v)
        {
            msg = "Hey Admin! This Train is Already Alloted in your required date and status is Inline! Please, Try to add another availble Trains.";
        }
        public TrainAlreadyAllotedException()
        {
            msg = "Hey Admin! This Train is Already Alloted in your required date and status is Inline! Please, Try to add another availble Trains.";
        }
        public override string Message => msg;
    }
}
