namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddTrainRouteDTO
    {
        public int RouteId { get; set; }
        public DateTime RouteStartDate { get; set; }
        public DateTime RouteEndDate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int StopNumber { get; set; }
        public float KilometerDistance { get; set; }
        public int TrainId { get; set; }
        public int StationId { get; set; }
        public int TrackId { get; set; }
        public int TrackNumber { get; set; }

    }
}
