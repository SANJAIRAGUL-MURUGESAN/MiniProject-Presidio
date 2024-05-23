using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;

namespace RailwayReservationApp.Interfaces
{
    public interface IAdminService
    {
        public Task<Train> AddTrain(AddTrainDTO addTrainDTO);
        public Task<TrainClass> AddTrainClass(AddTrainClassDTO addTrainClassDTO);
        public Task<TrainRoutes> AddTrainRoutes(AddTrainRouteDTO addTrainRouteDTO);
        public Task<Station> AddStation(AddStationDTO addStationDTO);

    }
}
