using System.ComponentModel.DataAnnotations;

namespace RailwayReservationApp.Models.UserDTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile Number must be exactly 10 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Disability is required.")]
        public bool Disability { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 100 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        [MinLength(6, ErrorMessage = "Password has to be minimum 6 characters long.")]
        public string Password { get; set; }
    }
}
