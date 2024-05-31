namespace RailwayReservationApp.Models.UserDTOs
{
    public class BookTrainDTO
    {
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public DateTime TrainDate { get; set; }
        public int UserId { get; set; }
        public int TrainId { get; set; }
        public List<int> Seats { get; set; }
        public string ClassName { get; set; }
    }
}
