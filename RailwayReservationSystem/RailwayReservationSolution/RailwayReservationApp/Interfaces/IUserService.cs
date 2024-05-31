using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;

namespace RailwayReservationApp.Interfaces
{
    public interface IUserService
    {
        public Task<UserRegisterReturnDTO> UserRegistration(UserRegisterDTO userRegisterDTO);
        public Task<IList<TrainSearchResultDTO>> SearchTrainByUser(SearchTrainDTO searchTrainDTO);
        public Task<BookTrainReturnDTO> BookTrainByUser(BookTrainDTO bookTrainDTO);
        public Task<AddPaymentReturnDTO> ConfirmPayment(AddPaymentDTO addPaymentDTO);
        public Task<CancelReservationReturnDTO> CancelReservation(CancelReservationDTO cancelReservationDTO);
        public Task<UserLoginReturnDTO> Login(UserLoginDTO userLoginDTO);
        public Task<IList<GetTrainClassReturnDTO>> GetAllClassofTrain(int TrainId);

    }
}
