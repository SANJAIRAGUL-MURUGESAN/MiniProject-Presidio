namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddTrainRouteDTO
    {
        public int RouteId { get; set; }
        public DateTime RouteDate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int StopNumber { get; set; }
        public float KilometerDistance { get; set; }
        public int TrainId { get; set; }
        public int StationId { get; set; }

    }
}
