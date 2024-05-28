namespace RailwayReservationApp.Exceptions.RewardExceptions
{
    public class NoRewardsFoundException : Exception
    {

        string msg;
        public NoRewardsFoundException()
        {
            msg = "No Rewards Found!";
        }
        public override string Message => msg;
    }
}
