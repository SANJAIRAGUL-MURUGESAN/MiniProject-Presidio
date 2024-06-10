using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AdminLoginDTO
    {
        [Required(ErrorMessage = "Admin Id cannot be Empty")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Password cannot be Empty")]
        [MinLength(5, ErrorMessage = "Password has to be minimum 6 char long")]
        public string Password { get; set; }
    }
}


