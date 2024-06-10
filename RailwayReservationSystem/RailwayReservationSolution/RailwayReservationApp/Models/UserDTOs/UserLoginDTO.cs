using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.UserDTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "User Id cannot be Empty")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Password cannot be Empty")]
        [MinLength(5, ErrorMessage = "Password has to be minimum 6 char long")]
        public string Password { get; set; }
    }
}
