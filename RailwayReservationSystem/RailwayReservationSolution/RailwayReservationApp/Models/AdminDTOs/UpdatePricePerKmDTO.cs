using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.AdminDTOs
{
    public class UpdatePricePerKmDTO
    {
        [Required(ErrorMessage = "Train Id is required.")]
        public int TrainId { get; set; }

        [Required(ErrorMessage = "PricePerKm is required.")]
        public float PricePerKm { get; set; }
    }
}
