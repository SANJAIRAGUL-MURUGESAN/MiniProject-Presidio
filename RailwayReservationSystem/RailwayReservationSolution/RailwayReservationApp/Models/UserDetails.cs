using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class UserDetails
    {
        [Key]
        public int UserDetailsId { get; set; }
        public int UserId { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHashKey { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }
        public string Status { get; set; }

    }
}
