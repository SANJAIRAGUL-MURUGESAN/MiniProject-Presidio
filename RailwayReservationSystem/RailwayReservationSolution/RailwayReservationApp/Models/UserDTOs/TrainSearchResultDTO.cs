namespace RailwayReservationApp.Models.UserDTOs
{
    public class TrainSearchResultDTO
    {
        public int TrainNumber { get; set; }
        public string TrainName { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TrainCapacity { get; set; }
    }
}
