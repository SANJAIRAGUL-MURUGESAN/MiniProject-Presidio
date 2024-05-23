namespace RailwayReservationApp.Models
{
    public class Track
    {
        public int TrackId { get; set; }
        public int TrackNumber { get; set; }
        public string TrackStatus { get; set; }

        // Foreign Key
        public int StationId { get; set; }
        public Station Station { get; set; }
    }
}
