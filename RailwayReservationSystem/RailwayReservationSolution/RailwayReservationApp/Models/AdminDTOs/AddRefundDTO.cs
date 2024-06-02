namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddRefundDTO
    {
        public float RefundAmount { get; set; }
        public string RefundDate { get; set; }
        public int ReservationCancelId { get; set; }
        public int UserId { get; set; }
    }
}
