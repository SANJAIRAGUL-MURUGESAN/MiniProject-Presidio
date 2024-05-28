namespace RailwayReservationApp.Models.UserDTOs
{
    public class CancelReservationReturnDTO
    {
        public string ReservationCancelReason { get; set; }
        public int SeatNumber { get; set; }
        public float RefundAmount { get; set; }
    }
}
