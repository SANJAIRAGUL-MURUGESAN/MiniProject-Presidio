using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RailwayReservationApp.Models
{
    public class Refund
    {
        [Key]
        public int RefundId { get; set; }
        public float RefundAmount { get; set; }
        public string RefundDate { get; set; }
        public int ReservationCancelId { get; set; }

        // Foreign key

        [ForeignKey("ReservationId")]
        public ReservationCancel ReservationCancel { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public Users Users { get; set; }

    }
}
