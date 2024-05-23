using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public float Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }


        // Foreign key - ReservationID
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
