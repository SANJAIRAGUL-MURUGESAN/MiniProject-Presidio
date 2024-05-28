using System.ComponentModel.DataAnnotations.Schema;

namespace RailwayReservationApp.Models.UserDTOs
{
    public class CancelReservationDTO
    {
        public string ReservationCancelReason { get; set; }
        public int SeatNumber { get; set; }
        public float RefundAmount { get; set; }
        public int UserId { get; set; }
        public int ReservationId { get; set; }

    }
}
