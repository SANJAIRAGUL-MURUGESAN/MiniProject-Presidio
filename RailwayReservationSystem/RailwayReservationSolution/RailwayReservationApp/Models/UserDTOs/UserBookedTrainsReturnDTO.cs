namespace RailwayReservationApp.Models.UserDTOs
{
    public class UserBookedTrainsReturnDTO
    {
        public int TrainNumber { get; set; }
        public string TrainName { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public DateTime TrainDate { get; set; }
        public int TrainCapacity { get; set; }
    }
}
