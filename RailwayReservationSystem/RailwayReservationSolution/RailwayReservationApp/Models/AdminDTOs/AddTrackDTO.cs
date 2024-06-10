using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddTrackDTO
    {
        public int TrackId { get; set; }

        [Required(ErrorMessage = "Track Number is required.")]
        public int TrackNumber { get; set; }

        [Required(ErrorMessage = "Track Status is required.")]
        public string TrackStatus { get; set; }

        [Required(ErrorMessage = "Track Starting Point is required.")]
        public string TrackStartingPoint { get; set; }

        [Required(ErrorMessage = "Track ENding Point is required.")]
        public string TrackEndingPoint { get; set; }

        [Required(ErrorMessage = "Station ID is required.")]
        public int StationId { get; set; }
    }
}
