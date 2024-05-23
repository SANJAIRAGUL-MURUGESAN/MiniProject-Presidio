using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class Station
    {
        [Key]
        public int StationId { get; set; }
        public string StationName { get; set; }
        public ICollection<Track> Tracks { get; set; }//No effect on the table
    }
}
