using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.AdminDTOs
{
    public class UpdateTrainRouteDetailsDTO
    {
        //[Required(ErrorMessage = "RouteId is required.")]
        public int RouteId { get; set; }

        [Required(ErrorMessage = "Route Start Date is required.")]
        public DateTime RouteStartDate { get; set; }

        [Required(ErrorMessage = "Route End Date is required.")]
        public DateTime RouteEndDate { get; set; }

        [Required(ErrorMessage = "Arrival Time is required.")]
        public DateTime ArrivalTime { get; set; }

        [Required(ErrorMessage = "Departure Time is required.")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Stop Number is required.")]
        public int StopNumber { get; set; }

        [Required(ErrorMessage = "Kilometer Distance is required.")]
        public float KilometerDistance { get; set; }

        [Required(ErrorMessage = "TrainID is required.")]
        public int TrainId { get; set; }

        [Required(ErrorMessage = "StationId is required.")]
        public int StationId { get; set; }

        [Required(ErrorMessage = "TackId is required.")]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "TrackNumber is required.")]
        public int TrackNumber { get; set; }
    }
}
