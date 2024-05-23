using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RailwayReservationApp.Models
{
    public class ReservationCancel
    {
        [Key]
        public int ReservationCancelId { get; set; }
        public string ReservationCancelReason { get; set; }
        public int SeatNumber { get; set; }
        public float RefundAmount { get; set; }

        // Foreign key
        public int UserId { get; set; }
        public Users User { get; set; }

        // Foreign key
        public int ReservationId { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

    }
}
