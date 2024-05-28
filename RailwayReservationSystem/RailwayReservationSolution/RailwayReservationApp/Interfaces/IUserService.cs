using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;

namespace RailwayReservationApp.Interfaces
{
    public interface IUserService
    {
        public Task<UserRegisterReturnDTO> UserRegistration(UserRegisterDTO userRegisterDTO);
        public Task<IEnumerable<Train>> SearchTrainByUser(SearchTrainDTO searchTrainDTO);
        public Task<BookTrainReturnDTO> BookTrainByUser(BookTrainDTO bookTrainDTO);
        public Task<AddPaymentReturnDTO> ConfirmPayment(AddPaymentDTO addPaymentDTO);
        public Task<CancelReservationReturnDTO> CancelReservation(CancelReservationDTO cancelReservationDTO);

    }
}
