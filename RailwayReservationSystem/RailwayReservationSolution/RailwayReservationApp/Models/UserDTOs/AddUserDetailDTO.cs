namespace RailwayReservationApp.Models.UserDTOs
{
    public class AddUserDetailDTO
    {
        public int UserId { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHashKey { get; set; }
        public string Status { get; set; }
    }
}
