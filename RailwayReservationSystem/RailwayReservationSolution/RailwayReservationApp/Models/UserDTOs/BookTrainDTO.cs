using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.UserDTOs
{
    public class BookTrainDTO
    {
        [Required(ErrorMessage = "StartingPoint is required.")]
        public string StartingPoint { get; set; }
        [Required(ErrorMessage = "Ending Point is required.")]
        public string EndingPoint { get; set; }
        [Required(ErrorMessage = "Train Date is required.")]
        public DateTime TrainDate { get; set; }
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "TrainId is required.")]
        public int TrainId { get; set; }
        public List<int> Seats { get; set; }
        [Required(ErrorMessage = "ClassName is required.")]
        public string ClassName { get; set; }
    }
}
