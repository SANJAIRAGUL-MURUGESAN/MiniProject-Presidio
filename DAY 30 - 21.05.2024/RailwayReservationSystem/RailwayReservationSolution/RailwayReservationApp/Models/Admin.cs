using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public bool Disability { get; set; }
        public string Address { get; set; }
    }
}
