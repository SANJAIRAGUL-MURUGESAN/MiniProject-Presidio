using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;

namespace RailwayReservationApp.Services
{
    public class AdminServices
    {
        private readonly IRepository<int, Train> _TrainRepository;
        private readonly IRepository<int, TrainRoutes> _TrainRouteRepository;
        private readonly IRepository<int, Station> _StationRepository;
        public AdminServices(IRepository<int, Train> repository, IRepository<int, TrainRoutes> trainRouteRepository, IRepository<int, Station> stationRepository)
        {
            _TrainRepository = repository;
            _TrainRouteRepository = trainRouteRepository;
            _StationRepository = stationRepository;
        }

        // Start - Function to Add Train by Admin
        public Train MapAddTrainDTOtoTrain(AddTrainDTO addTrainDTO)
        {
            Train train = new Train();
            //train.TrainId = addTrainDTO.TrainId;
            train.TrainNumber = addTrainDTO.TrainNumber;
            train.TrainName = addTrainDTO.TrainName;
            train.TrainStartDate = addTrainDTO.TrainStartDate;
            train.TrainEndDate = addTrainDTO.TrainStartDate;
            train.StartingPoint = addTrainDTO.StartingPoint;
            train.EndingPoint = addTrainDTO.EndingPoint;
            train.ArrivalTime = addTrainDTO.ArrivalTime;
            train.DepartureTime = addTrainDTO.DepartureTime;
            train.TotalSeats = addTrainDTO.TotalSeats;
            train.PricePerKM = addTrainDTO.PricePerKM;
            train.TrainStatus = addTrainDTO.TrainStatus;
            return train;
        }

        public AddTrainReturnDTO MapResultToAddTrainReturnDTO(Train result)
        {
            AddTrainReturnDTO addTrainReturnDTO = new AddTrainReturnDTO();
            addTrainReturnDTO.TrainNumber = result.TrainNumber;
            addTrainReturnDTO.TrainName = result.TrainName;
            addTrainReturnDTO.TrainStartDate = result.TrainStartDate;
            addTrainReturnDTO.TrainEndDate = result.TrainEndDate;
            addTrainReturnDTO.StartingPoint = result.StartingPoint;
            addTrainReturnDTO.EndingPoint = result.EndingPoint;
            addTrainReturnDTO.ArrivalTime = result.ArrivalTime;
            addTrainReturnDTO.DepartureTime = result.DepartureTime;
            addTrainReturnDTO.TotalSeats = result.TotalSeats;
            addTrainReturnDTO.PricePerKM = result.PricePerKM;
            addTrainReturnDTO.TrainStatus = result.TrainStatus;
            return addTrainReturnDTO;
        }
        public async Task<AddTrainReturnDTO> AddTrainbyAdmin(AddTrainDTO addTrainDTO)
        {
            try
            {
                var Trains = (await _TrainRepository.Get()).Where(t => t.TrainNumber == addTrainDTO.TrainNumber && t.TrainStatus == "Inline");
                if (Trains != null)
                {
                    foreach (var train in Trains)
                    {
                        if(train.TrainStartDate == addTrainDTO.TrainStartDate || train.TrainStartDate == addTrainDTO.TrainEndDate)
                        {
                            throw new TrainAlreadyAllotedException();
                        }
                    }
                }
                Train trains = MapAddTrainDTOtoTrain(addTrainDTO);
                var result = await _TrainRepository.Add(trains);
                return MapResultToAddTrainReturnDTO(result);
            }
            catch(Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        // End - Function to Add Train by Admin

        // Start - Function to add Train Route by Admin

        public TrainRoutes MapAddTrainRouteDTOtoTrainRoute(AddTrainRouteDTO addTrainReturnDTO)
        {
            TrainRoutes trainRoutes = new TrainRoutes();
            trainRoutes.RouteId = addTrainReturnDTO.RouteId;
            trainRoutes.RouteDate = addTrainReturnDTO.RouteDate;
            trainRoutes.ArrivalTime = addTrainReturnDTO.ArrivalTime;
            trainRoutes.DepartureTime = addTrainReturnDTO.DepartureTime;
            trainRoutes.StopNumber = addTrainReturnDTO.StopNumber;
            trainRoutes.KilometerDistance = addTrainReturnDTO.KilometerDistance;
            trainRoutes.TrainId = addTrainReturnDTO.TrainId;
            trainRoutes.StationId = addTrainReturnDTO.StationId;
            return trainRoutes;
        }

        public AddTrainRouteReturnDTO MapResulttoAddTrainRouteReturnDTO(TrainRoutes trainRoutes)
        {
            AddTrainRouteReturnDTO addTrainRouteReturnDTO = new AddTrainRouteReturnDTO();
            addTrainRouteReturnDTO.RouteDate = trainRoutes.RouteDate;
            addTrainRouteReturnDTO.ArrivalTime = trainRoutes.ArrivalTime;
            addTrainRouteReturnDTO.DepartureTime = trainRoutes.DepartureTime;
            addTrainRouteReturnDTO.StopNumber = trainRoutes.StopNumber;
            addTrainRouteReturnDTO.KilometerDistance = trainRoutes.KilometerDistance;
            addTrainRouteReturnDTO.TrainId = trainRoutes.TrainId;
            addTrainRouteReturnDTO.StationId = trainRoutes.StationId;
            return addTrainRouteReturnDTO;
        }

        public async Task<AddTrainRouteReturnDTO> AddTrainRoutebyAdmin(AddTrainRouteDTO addTrainRouteDTO)
        {
            try
            {
                var train = await _TrainRouteRepository.GetbyKey(addTrainRouteDTO.TrainId);
                if(train != null)
                {
                    var station = await _StationRepository.GetbyKey(addTrainRouteDTO.StationId);
                    TrainRoutes trainRoutes = MapAddTrainRouteDTOtoTrainRoute(addTrainRouteDTO);
                    var trainRoute = await _TrainRouteRepository.Add(trainRoutes);
                    return MapResulttoAddTrainRouteReturnDTO(trainRoute);
                }
                throw new NoSuchTrainFoundException();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

    }
}
