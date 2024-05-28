namespace RailwayReservationApp.Models
{
    public class Track
    {
        public int TrackId { get; set; }
        public int TrackNumber { get; set; }
        public string TrackStatus { get; set; }
        public string TrackStartingPoint { get; set; }
        public string TrackEndingPoint { get; set; }

        // Foreign Key
        public int StationId { get; set; }
        public Station Station { get; set; }

        // Foreign Key
        public ICollection<TrackReservation> TrackReservations { get; set; }//No effect on the table
    }
}
