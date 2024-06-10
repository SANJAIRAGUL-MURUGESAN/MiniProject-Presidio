using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RailwayReservationApp.Models.UserDTOs
{
    public class CancelReservationDTO
    {
        [Required(ErrorMessage = "Cancelling reason is required.")]
        public string ReservationCancelReason { get; set; }
        [Required(ErrorMessage = "Seat Number is required.")]
        public int SeatNumber { get; set; }
        [Required(ErrorMessage = "Refund amount is required.")]
        public float RefundAmount { get; set; }
        [Required(ErrorMessage = "User Id is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "ReservationID is required.")]
        public int ReservationId { get; set; }

    }
}
