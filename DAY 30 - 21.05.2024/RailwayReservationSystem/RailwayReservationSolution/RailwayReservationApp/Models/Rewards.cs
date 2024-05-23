using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class Rewards
    {
        [Key]
        public int RewardId { get; set; }
        public float RewardPoints { get; set; }

        // Foreign key
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
