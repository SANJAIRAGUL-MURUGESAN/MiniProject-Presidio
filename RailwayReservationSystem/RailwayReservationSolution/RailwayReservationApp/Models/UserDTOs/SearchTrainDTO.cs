using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.UserDTOs
{
    public class SearchTrainDTO
    {
        [Required(ErrorMessage = "Starting Point is required.")]
        public string StartingPoint { get; set; }
        [Required(ErrorMessage = "Ending Point is required.")]
        public string EndingPoint { get; set; }
        [Required(ErrorMessage = "Train Start Date is required.")]
        public DateTime TrainStartDate { get; set; }
    }
}
