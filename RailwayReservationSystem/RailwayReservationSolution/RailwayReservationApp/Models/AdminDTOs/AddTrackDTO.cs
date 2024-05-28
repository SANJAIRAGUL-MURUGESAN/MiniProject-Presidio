namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddTrackDTO
    {
        public int TrackId { get; set; }
        public int TrackNumber { get; set; }
        public string TrackStatus { get; set; }
        public string TrackStartingPoint { get; set; }
        public string TrackEndingPoint { get; set; }
        public int StationId { get; set; }
    }
}
