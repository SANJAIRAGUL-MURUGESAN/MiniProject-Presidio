namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddRefundReturnDTO
    {
        public float RefundAmount { get; set; }
        public string RefundDate { get; set; }
        public int ReservationCancelId { get; set; }
        public ReservationCancel ReservationCancel { get; set; }
        public int UserId { get; set; }
    }
}
