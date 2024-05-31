using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;

namespace RailwayReservationApp.Interfaces
{
    public interface IUserSecondaryService
    {
        public Task<IList<UserBookedTrainsReturnDTO>> GetBookedTrains(int UserId);
        public Task<IList<UserBookedTrainsReturnDTO>> GetPastBookings(int UserId);
        public Task<Users> UpdateUser(UpdateUserDTO updateUserDTO);
        public Task<string> DeleteUser(int UserId);
        public Task<CheckSeatDetailsReturnDTO> CheckSeatsDetailsbyAdmin(int TrainId);
    }
}
