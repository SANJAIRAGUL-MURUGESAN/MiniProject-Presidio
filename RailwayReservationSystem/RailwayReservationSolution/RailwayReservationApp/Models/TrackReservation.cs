namespace RailwayReservationApp.Models
{
    public class TrackReservation
    {
        public int TrackReservationId { get; set; }
        public DateTime? TrackOccupiedStartTime { get; set; } = null;
        public DateTime? TrackOccupiedEndTime { get; set; } = null;
        public DateTime? TrackOccupiedStartDate { get; set; } = null;
        public DateTime? TrackOccupiedEndDate { get; set; } = null;
        public string ReservationStatus{get;set;}

        // Foreign Key
        public int TrackId { get; set; }
        public Track Track { get; set; }

        // Foreign Key

        public int TrainId { get; set; }
    }
}
