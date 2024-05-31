using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;

namespace RailwayReservationApp.Interfaces
{
    public interface IAdminService
    {
        public Task<AddTrainReturnDTO> AddTrainbyAdmin(AddTrainDTO addTrainDTO);
        public Task<AddTrainClassReturnDTO> AddTrainClassbyAdmin(AddTrainClassDTO addTrainClassDTO);
        public Task<AddTrainRouteReturnDTO> AddTrainRoutebyAdmin(AddTrainRouteDTO addTrainRouteDTO);
        public Task<AddStationReturnDTO> AddStationbyAdmin(AddStationDTO addStationDTO);
        public Task<AddTrackReturnDTO> AddTrackToStationbyAdmin(AddTrackDTO addTrackDTO);
        public Task<UpdatePricePerKmReturnDTO> UpdatePricePerKmbyAdmin(UpdatePricePerKmDTO updatePricePerKm);
        public Task<CheckSeatDetailsReturnDTO> CheckSeatsDetailsbyAdmin(int TrainId);
        public Task<GetReservedTracksReturnDTO> GetReservedTracksofStationbyAdmin(int StationId);
        public Task<string> UpdateTrainStatusCompleted(int TrainId);
        public Task<TrainRoutes> UpdateTrainRouteDetails(UpdateTrainRouteDetailsDTO updateTrainRouteDetailsDTO);
        public Task<IList<Train>> GetAllInlineTrains();
        public Task<AdminRegisterReturnDTO> AdminRegistration(AdminRegisterDTO adminRegisterDTO);
        public Task<AdminLoginReturnDTO> AdminLogin(AdminLoginDTO adminLoginDTO);
    }
}
