using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models
{
    public class Train
    {
        [Key]
        public int TrainId { get; set; }
        public string TrainName { get; set; }
        public int TrainNumber { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public DateTime TrainStartDate { get; set; }
        public DateTime TrainEndDate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int TotalSeats { get; set; }
        public float PricePerKM { get; set; }
        public string TrainStatus { get; set; }

        public ICollection<TrainRoutes> TrainRoutes { get; set; }//No effect on the table
        public ICollection<TrainClass> TrainClasses { get; set; }// No effect on the table
        public ICollection<Reservation> TrainReservations { get; set; }//No effect on the table

    }
}
