namespace RailwayReservationApp.Models.AdminDTOs
{
    public class CheckSeatDetailsReturnDTO
    {
        public int TotalSeat { get; set; }
        public List<int>  ReservedSeats{ get;set; }
        public List<int> AvailableSeats { get; set; }
    }
}
