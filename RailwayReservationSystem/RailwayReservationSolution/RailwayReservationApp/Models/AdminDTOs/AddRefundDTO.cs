using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddRefundDTO
    {
        [Required(ErrorMessage = "Refund Amount is required.")]
        public float RefundAmount { get; set; }

        [Required(ErrorMessage = "Refund Date is required.")]
        public string RefundDate { get; set; }

        [Required(ErrorMessage = "ReservationCancel Id is required.")]
        public int ReservationCancelId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}
