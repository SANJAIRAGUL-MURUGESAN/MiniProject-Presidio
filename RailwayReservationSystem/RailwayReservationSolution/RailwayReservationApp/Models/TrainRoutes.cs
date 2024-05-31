using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class TrainRoutes
    {
        [Key]
        public int RouteId { get; set; }
        public DateTime RouteStartDate { get; set; }
        public DateTime RouteEndDate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int StopNumber { get; set; }
        public float KilometerDistance { get; set; }

        // ForeignKey - Train ID
        public int TrainId { get; set; }
        public Train Train { get; set; }

        // ForeignKey - Sation ID
        public int StationId { get; set; }
        public Station Station { get; set; }

        // ForeignKey - Track ID
        public int TrackId { get; set; }
        public int TrackNumber { get; set; }
    }
}
