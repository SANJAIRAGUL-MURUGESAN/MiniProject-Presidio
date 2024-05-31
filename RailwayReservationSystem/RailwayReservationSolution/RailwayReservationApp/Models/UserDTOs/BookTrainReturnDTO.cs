namespace RailwayReservationApp.Models.UserDTOs
{
    public class BookTrainReturnDTO
    {
        public DateTime ReservationDate { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public DateTime TrainDate { get; set; }
        public float Amount { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public int TrainId { get; set; }
        public List<int> Seats { get; set; }
        public string ClassName { get; set; }
    }
}
