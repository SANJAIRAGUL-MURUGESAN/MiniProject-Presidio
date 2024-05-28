namespace RailwayReservationApp.Models.UserDTOs
{
    public class AddPaymentReturnDTO
    {
        public DateTime PaymentDate { get; set; }
        public float Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public int ReservationId { get; set; }
    }
}
