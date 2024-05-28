namespace RailwayReservationApp.Exceptions.RewardExceptions
{
    public class NoSuchRewardFoundException : Exception
    {
        string msg;
        public NoSuchRewardFoundException()
        {
            msg = "No Such Reward Found!";
        }
        public override string Message => msg;
    }
}
