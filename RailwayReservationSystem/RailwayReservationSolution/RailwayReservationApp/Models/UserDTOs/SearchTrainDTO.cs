namespace RailwayReservationApp.Models.UserDTOs
{
    public class SearchTrainDTO
    {
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public DateTime TrainStartDate { get; set; }
    }
}
