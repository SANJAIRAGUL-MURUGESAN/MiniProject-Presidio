using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public bool Disability { get; set; }
        public string Address { get; set; }
        public ICollection<Reservation> Reservations { get; set; }//No effect on the table
        public ICollection<ReservationCancel> ReservationCancels { get; set; }//No effect on the table
        public ICollection<Refund> Refunds { get; set; }//No effect on the table
    }
}
