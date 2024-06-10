using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddTrainDTO
    {
        public int TrainId { get; set; }

        [Required(ErrorMessage = "Train Name is required.")]
        public string TrainName { get; set; }

        [Required(ErrorMessage = "Train Number is required.")]
        public int TrainNumber { get; set; }

        [Required(ErrorMessage = "Starting Point is required.")]
        public string StartingPoint { get; set; }

        [Required(ErrorMessage = "Ending Point is required.")]
        public string EndingPoint { get; set; }

        [Required(ErrorMessage = "Train Start Date is required.")]
        public DateTime TrainStartDate { get; set; }

        [Required(ErrorMessage = "Train End Date is required.")]
        public DateTime TrainEndDate { get; set; }

        [Required(ErrorMessage = "Train Arrival Time is required.")]
        public DateTime ArrivalTime { get; set; }

        [Required(ErrorMessage = "Train Departure Time is required.")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Train Total Seats is required.")]
        public int TotalSeats { get; set; }

        [Required(ErrorMessage = "Train Price Per kmis required.")]
        public float PricePerKM { get; set; }

        [Required(ErrorMessage = "Train Status is required.")]
        public string TrainStatus { get; set; }
    }
}
