namespace RailwayReservationApp.Models
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int SeatNumber { get; set; }

        //Foreign Key
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
