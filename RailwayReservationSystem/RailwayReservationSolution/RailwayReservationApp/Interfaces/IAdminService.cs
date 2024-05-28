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

    }
}
