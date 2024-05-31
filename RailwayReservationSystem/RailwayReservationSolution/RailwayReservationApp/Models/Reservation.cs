using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public DateTime TrainDate { get; set; }
        public float Amount { get; set; }
        public string Status { get; set; }
        public string TrainClassName { get; set; }

        // Foreign key - User ID
        public int UserId { get; set; }
        public Users User { get; set; }

        // Foreign key - Train ID
        public int TrainId { get; set; }
        public Train Train { get; set; }

        public ICollection<Payment> Payments { get; set; }//No effect on the table
        public ICollection<ReservationCancel> ReservationCancels {get;set;}// No effect on the table

        public ICollection<Seat> Seats { get; set; }//No effect on the table
    }
}
