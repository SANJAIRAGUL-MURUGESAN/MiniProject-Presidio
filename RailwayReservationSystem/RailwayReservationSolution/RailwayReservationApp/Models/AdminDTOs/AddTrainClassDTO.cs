using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddTrainClassDTO
    {
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Train Class Name is required.")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Train Class Price is required.")]
        public float ClassPrice { get; set; }

        [Required(ErrorMessage = "Train Start Seat Number is required.")]
        public int StartingSeatNumber { get; set; }

        [Required(ErrorMessage = "Train Start End Number is required.")]
        public int EndingSeatNumber { get; set; }


        [Required(ErrorMessage = "TrainID is required.")]
        public int TrainId { get; set; }
    }
}
