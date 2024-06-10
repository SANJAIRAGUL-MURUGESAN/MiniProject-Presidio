using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddStationDTO
    {
        public int StationId { get; set; }
        [Required(ErrorMessage = "Station Name is required.")]
        public string StationName { get; set; }

        [Required(ErrorMessage = "Station State is required.")]
        public string StationState { get; set; }

        [Required(ErrorMessage = "Station Pincode is required.")]
        public string StationPincode { get; set; }
    }
}
