using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.UserDTOs
{
    public class AddPaymentDTO
    {
        public int PaymentId { get; set; }
        [Required(ErrorMessage = "Payment Date is required.")]
        public DateTime PaymentDate { get; set; }
        [Required(ErrorMessage = "Payment Amount is required.")]
        public float Amount { get; set; }
        [Required(ErrorMessage = "Payment Method is required.")]
        public string PaymentMethod { get; set; }
        [Required(ErrorMessage = "Payment Status is required.")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Reservation Id is required.")]
        public int ReservationId { get; set; }
    }
}
