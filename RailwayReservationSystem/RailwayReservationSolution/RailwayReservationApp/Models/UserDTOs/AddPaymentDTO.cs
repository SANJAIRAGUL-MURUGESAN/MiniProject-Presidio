namespace RailwayReservationApp.Models.UserDTOs
{
    public class AddPaymentDTO
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public float Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public int ReservationId { get; set; }
    }
}
