namespace RailwayReservationApp.Exceptions.SeatExcepions
{
    public class InvalidSeatAllocationException : Exception
    {
        string msg;
        public InvalidSeatAllocationException()
        {
            msg = "Invalid Seat Allocation Admin! Recheck and add valid seats";
        }
        public override string Message => msg;
    }
}
