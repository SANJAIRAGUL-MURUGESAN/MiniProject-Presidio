using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class TrainClass
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public float ClassPrice { get; set; }
        public int StartingSeatNumber { get; set; }
        public int EndingSeatNumber { get; set; }

        // Foreign key - Train ID
        public int TrainId {get;set;}
        public Train Train { get; set; }
    }
}
