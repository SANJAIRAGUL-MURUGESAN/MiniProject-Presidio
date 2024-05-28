using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Exceptions.StationExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Exceptions.TrainClassExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Repositories;
using RailwayReservationApp.Repositories.StationRequest;
using RailwayReservationApp.Repositories.TrackRequest;
using RailwayReservationApp.Repositories.TrainRequest;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RailwayReservationApp.Services
{
    public class AdminServices : IAdminService
    {
        private readonly IRepository<int, Train> _TrainRepository;
        private readonly IRepository<int, TrainRoutes> _TrainRouteRepository;
        private readonly IRepository<int, Station> _StationRepository;
        private readonly IRepository<int, TrainClass> _TrainClassRepository;
        private readonly IRepository<int, Track> _TrackRepository;
        private readonly IRepository<int, TrackReservation> _TrackReservationRepository;
        private readonly StationRequestRepository _StationRequestRepository; // To retrieve Tracks for a station
        private readonly TrainRequestforClassesRepository _TrainRequestforClassesRepository; // To retrieve for a Classes for a Train
        private readonly TrackRequestforReservationRepository _TrackRequestforReservationRepository; // To retrieve Reservation for a Track
        public AdminServices(IRepository<int, Train> repository, IRepository<int, TrainRoutes> trainRouteRepository, IRepository<int, Station> stationRepository,
            StationRequestRepository stationRequestRepository, TrainRequestforClassesRepository trainRequestforClassesRepository,
            IRepository<int,Track> trackRepository, TrackRequestforReservationRepository trackRequestforReservationRepository,
            IRepository<int, TrackReservation> trackReservationRepository, IRepository<int, TrainClass> trainClassRepository)
        {
            _TrainRepository = repository;
            _TrainRouteRepository = trainRouteRepository;
            _StationRepository = stationRepository;
            _StationRequestRepository = stationRequestRepository;
            _TrainRequestforClassesRepository = trainRequestforClassesRepository;
            _TrackRepository = trackRepository;
            _TrackReservationRepository = trackReservationRepository;
            _TrackRequestforReservationRepository = trackRequestforReservationRepository;
            _TrainClassRepository = trainClassRepository;
        }

        // Start - Function to Add Train by Admin
        public Train MapAddTrainDTOtoTrain(AddTrainDTO addTrainDTO)
        {
            Train train = new Train();
            //train.TrainId = addTrainDTO.TrainId;
            train.TrainNumber = addTrainDTO.TrainNumber;
            train.TrainName = addTrainDTO.TrainName;
            train.TrainStartDate = addTrainDTO.TrainStartDate;
            train.TrainEndDate = addTrainDTO.TrainEndDate;
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
                // Checking whether the Train status is already in Inline and if Inline, checking whether
                // -it is available in required date
                var Trains = (await _TrainRepository.Get()).Where(t => t.TrainNumber == addTrainDTO.TrainNumber && t.TrainStatus == "Inline");
                if (Trains != null)
                {
                    foreach (var train in Trains)
                    {
                        if(addTrainDTO.TrainStartDate.Date >= train.TrainStartDate.Date && addTrainDTO.TrainStartDate.Date <= train.TrainEndDate.Date)
                        {
                            throw new TrainAlreadyAllotedException();
                        }
                        if(addTrainDTO.TrainEndDate.Date >= train.TrainStartDate.Date && addTrainDTO.TrainEndDate.Date <= train.TrainEndDate.Date)
                        {
                            throw new TrainAlreadyAllotedException();
                        }
                    }
                }
                Train trains = MapAddTrainDTOtoTrain(addTrainDTO);
                var result = await _TrainRepository.Add(trains);
                return MapResultToAddTrainReturnDTO(result);
            }
            catch(TrainAlreadyAllotedException taae)
            {
                throw new TrainAlreadyAllotedException();
            }
        }

        // End - Function to Add Train by Admin

        // Start - Function to add Train Route by Admin

        public TrainRoutes MapAddTrainRouteDTOtoTrainRoute(AddTrainRouteDTO addTrainReturnDTO)
        {
            TrainRoutes trainRoutes = new TrainRoutes();
            trainRoutes.RouteId = addTrainReturnDTO.RouteId;
            trainRoutes.RouteStartDate = addTrainReturnDTO.RouteStartDate;
            trainRoutes.RouteEndDate = addTrainReturnDTO.RouteEndDate;
            trainRoutes.ArrivalTime = addTrainReturnDTO.ArrivalTime;
            trainRoutes.DepartureTime = addTrainReturnDTO.DepartureTime;
            trainRoutes.StopNumber = addTrainReturnDTO.StopNumber;
            trainRoutes.KilometerDistance = addTrainReturnDTO.KilometerDistance;
            trainRoutes.TrainId = addTrainReturnDTO.TrainId;
            trainRoutes.StationId = addTrainReturnDTO.StationId;
            trainRoutes.TrackId = addTrainReturnDTO.TrackId;
            trainRoutes.TrackNumber = addTrainReturnDTO.TrackNumber;
            return trainRoutes;
        }

        public AddTrainRouteReturnDTO MapResulttoAddTrainRouteReturnDTO(TrainRoutes trainRoutes)
        {
            AddTrainRouteReturnDTO addTrainRouteReturnDTO = new AddTrainRouteReturnDTO();
            addTrainRouteReturnDTO.RouteStartDate = trainRoutes.RouteStartDate;
            addTrainRouteReturnDTO.RouteEndDate = trainRoutes.RouteEndDate;
            addTrainRouteReturnDTO.ArrivalTime = trainRoutes.ArrivalTime;
            addTrainRouteReturnDTO.DepartureTime = trainRoutes.DepartureTime;
            addTrainRouteReturnDTO.StopNumber = trainRoutes.StopNumber;
            addTrainRouteReturnDTO.KilometerDistance = trainRoutes.KilometerDistance;
            addTrainRouteReturnDTO.TrainId = trainRoutes.TrainId;
            addTrainRouteReturnDTO.StationId = trainRoutes.StationId;
            addTrainRouteReturnDTO.TrackId = trainRoutes.TrackId;
            addTrainRouteReturnDTO.TrackNumber = trainRoutes.TrackNumber;
            return addTrainRouteReturnDTO;
        }

        public bool CheckReservationAvailable(List<TrackReservation> trackReservation, AddTrainRouteDTO addTrainRouteDTO)
        {
            foreach(var Reservation in trackReservation)
            {
                if(addTrainRouteDTO.RouteStartDate >= Reservation.TrackOccupiedStartDate && addTrainRouteDTO.RouteStartDate <= Reservation.TrackOccupiedEndDate)
                {
                    return false;
                }
                if (addTrainRouteDTO.RouteEndDate >= Reservation.TrackOccupiedStartDate && addTrainRouteDTO.RouteEndDate <= Reservation.TrackOccupiedEndDate)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<TrackReservation> TrackReservationAddition(TrainRoutes trainRoutes)
        {
            TrackReservation trackReservation = new TrackReservation();
            trackReservation.TrackId = trainRoutes.TrackId;
            trackReservation.TrackOccupiedStartDate = trainRoutes.RouteStartDate;
            trackReservation.TrackOccupiedEndDate = trainRoutes.RouteEndDate;
            trackReservation.TrackOccupiedStartTime = trainRoutes.RouteStartDate;
            trackReservation.TrackOccupiedEndTime = trainRoutes.RouteEndDate;
            trackReservation.ReservationStatus = "Inline";
            var Result = await _TrackReservationRepository.Add(trackReservation);
            return trackReservation;
        }

        public async Task<AddTrainRouteReturnDTO> AddTrainRoutebyAdmin(AddTrainRouteDTO addTrainRouteDTO)
        {
            try
            {
                // Checking whether the Train is available in the DB or not
                // Here Train availability will be already checked and already added
                var train = await _TrainRepository.GetbyKey(addTrainRouteDTO.TrainId);
                var station = await _StationRepository.GetbyKey(addTrainRouteDTO.StationId);
                if(station == null)
                {
                    throw new NoSuchStationFoundException();
                }
                if(train != null)
                {
                    // Fetching the tracks for the station Provided by Admin
                    var StationTracks = await _StationRequestRepository.GetbyKey(addTrainRouteDTO.StationId);
                    // Fetching the tracks that are available (Already Track will be added by Admin)
                    List<Track> TrackList = StationTracks.Tracks.ToList();
                    Console.WriteLine("TrackCount :"+TrackList.Count);
                    if (TrackList.Count > 0)
                    {
                        bool ReservationResult = true; // If identify desired track is available or not 
                        foreach (var Track in TrackList)
                        {
                            if (Track.TrackNumber == addTrainRouteDTO.TrackNumber)
                            {
                                var TrackReservation = await _TrackRequestforReservationRepository.GetbyKey(addTrainRouteDTO.TrackId);
                                ReservationResult = CheckReservationAvailable(TrackReservation.TrackReservations.Where(r => r.ReservationStatus == "Inline").ToList(),addTrainRouteDTO);
                                if(ReservationResult == true)
                                {
                                    TrainRoutes trainRoutes = MapAddTrainRouteDTOtoTrainRoute(addTrainRouteDTO);
                                    var RoutedAddedResult = await _TrainRouteRepository.Add(trainRoutes);
                                    var TrackReservationAddResult = TrackReservationAddition(trainRoutes);
                                    return MapResulttoAddTrainRouteReturnDTO(RoutedAddedResult);
                                }
                            }
                        }
                        if(ReservationResult == false)
                        {
                            throw new RequiredTrackBusyException();
                        }
                    }
                    throw new NoTrainTracksFoundException();
                }
                throw new NoSuchTrainFoundException();
            }
            catch(NoSuchTrainFoundException nsfe)
            {
                throw new NoSuchTrainFoundException();
            }
            catch (NoSuchStationFoundException nssfe)
            {
                throw new NoSuchStationFoundException();
            }
        }

        // End - Function to add Train Route by Admin

        // Start - Function to add Train Class by Admin

        public TrainClass MapAddTrainClassDTOToTrainClass(AddTrainClassDTO addTrainClassDTO)
        {
            TrainClass trainClass = new TrainClass();
            trainClass.ClassId = addTrainClassDTO.ClassId;
            trainClass.ClassName = addTrainClassDTO.ClassName;
            trainClass.ClassPrice = addTrainClassDTO.ClassPrice;
            trainClass.StartingSeatNumber = addTrainClassDTO.StartingSeatNumber;
            trainClass.EndingSeatNumber = addTrainClassDTO.EndingSeatNumber;
            trainClass.TrainId = addTrainClassDTO.TrainId;
            return trainClass;
        }

        public AddTrainClassReturnDTO MapResulToAddTrainClassReturnDTO(TrainClass Result)
        {
            AddTrainClassReturnDTO addTrainClassReturnDTO = new AddTrainClassReturnDTO();
            addTrainClassReturnDTO.TrainId = Result.TrainId;
            addTrainClassReturnDTO.ClassName = Result.ClassName;
            addTrainClassReturnDTO.ClassPrice = Result.ClassPrice;
            addTrainClassReturnDTO.StartingSeatNumber = Result.StartingSeatNumber;
            addTrainClassReturnDTO.EndingSeatNumber = Result.EndingSeatNumber;
            return addTrainClassReturnDTO;
        }
        public async Task<AddTrainClassReturnDTO> AddTrainClassbyAdmin(AddTrainClassDTO addTrainClassDTO)
        {
            try
            {
                // Checking whether the Train is available in the DB or not
                // Here Train availability will be already checked and already added
                var train = await _TrainRepository.GetbyKey(addTrainClassDTO.TrainId);
                if(train != null)
                {
                    // Fetching Classes(if available) for a Train
                    var TrainResult = await _TrainRequestforClassesRepository.GetbyKey(addTrainClassDTO.TrainId);
                    List<TrainClass> trainClasses = TrainResult.TrainClasses.ToList();
                    int Flag = 0;
                    if (trainClasses.Count > 0)
                    {
                        foreach(var classes in trainClasses)
                        {
                            if(addTrainClassDTO.StartingSeatNumber >= classes.StartingSeatNumber && addTrainClassDTO.StartingSeatNumber <= classes.EndingSeatNumber )
                            {
                                Flag = 1;
                                break;
                            }
                            if (addTrainClassDTO.EndingSeatNumber >= classes.StartingSeatNumber && addTrainClassDTO.EndingSeatNumber <= classes.EndingSeatNumber)
                            {
                                Flag = 1;
                                break;
                            }
                            if(addTrainClassDTO.StartingSeatNumber > train.TotalSeats || addTrainClassDTO.EndingSeatNumber > train.TotalSeats)
                            {
                                Flag = 1;
                                break;
                            }
                        }
                    }
                    if (Flag == 1)
                    {
                        throw new InvalidSeatAllocationException();
                    }
                    TrainClass trainClass = MapAddTrainClassDTOToTrainClass(addTrainClassDTO);
                    var result = await _TrainClassRepository.Add(trainClass);
                    return MapResulToAddTrainClassReturnDTO(result);
                }
                throw new NoSuchTrainFoundException();
            }
            catch(NoSuchTrainClassFoundException nstfe)
            {
                throw new NoSuchTrainClassFoundException();
            }
        }

        // End - Function to add Train Class by Admin

        // Start - Function to add Station by Admin

        public Station MapAddStationDTOtoStation(AddStationDTO addStationDTO)
        {
            Station station = new Station();
            station.StationId = addStationDTO.StationId;
            station.StationName = addStationDTO.StationName;
            station.StationState = addStationDTO.StationState;
            station.StationPincode = addStationDTO.StationPincode;
            return station;
        }
        public AddStationReturnDTO MapResultToAddStationReturnDTO(Station station)
        {
            AddStationReturnDTO addStationReturnDTO = new AddStationReturnDTO();
            addStationReturnDTO.StationName = station.StationName;
            addStationReturnDTO.StationState = station.StationState;
            addStationReturnDTO.StationPincode = station.StationPincode;
            return addStationReturnDTO;
        }

        public async Task<AddStationReturnDTO> AddStationbyAdmin(AddStationDTO addStationDTO)
        {
            try
            {
                var stations = (await _StationRepository.Get()).Where(s => s.StationState == addStationDTO.StationState);
                foreach(var station in stations)
                {
                    if(station.StationName == addStationDTO.StationName)
                    {
                        throw new StationAlreadyAddedException();
                    }
                }
                Station Mappedstation = MapAddStationDTOtoStation(addStationDTO);
                var result = await _StationRepository.Add(Mappedstation);
                return MapResultToAddStationReturnDTO(result);
            }
            catch (StationAlreadyAddedException saae)
            {
                throw new StationAlreadyAddedException();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        // End - Function to add Station by Admin

        // Start - Function to add Track to a station by Admin

        public Track MapAddTrackDTOtoTrack(AddTrackDTO addTrackDTO)
        {
            Track track = new Track();
            track.TrackNumber = addTrackDTO.TrackNumber;
            track.TrackStartingPoint = addTrackDTO.TrackStartingPoint;
            track.TrackEndingPoint = addTrackDTO.TrackEndingPoint;
            track.StationId = addTrackDTO.StationId;
            track.TrackStatus = addTrackDTO.TrackStatus;
            return track;
        }

        public AddTrackReturnDTO MapResultToAddTrackReturnDTO(Track track)
        {
            AddTrackReturnDTO addTrackReturnDTO = new AddTrackReturnDTO();
            addTrackReturnDTO.TrackNumber = track.TrackNumber;
            addTrackReturnDTO.TrackStartingPoint = track.TrackStartingPoint;
            addTrackReturnDTO.TrackEndingPoint = track.TrackEndingPoint;
            addTrackReturnDTO.TrackStatus = track.TrackStatus;
            addTrackReturnDTO.StationId = track.StationId;
            return addTrackReturnDTO;
        }
        public async Task<AddTrackReturnDTO> AddTrackToStationbyAdmin(AddTrackDTO addTrackDTO)
        {
            try
            {
                var AvailableTracks = await _StationRequestRepository.GetbyKey(addTrackDTO.StationId);
                if (AvailableTracks != null)
                {
                    List<Track> TrackList = AvailableTracks.Tracks.ToList();
                    if (TrackList.Count > 0)
                    {
                        foreach (var tracks in TrackList)
                        {
                            if (tracks.TrackNumber == addTrackDTO.TrackNumber)
                            {
                                throw new TrackAlreadyAddedException();
                            }
                        }
                    }
                    Track track = MapAddTrackDTOtoTrack(addTrackDTO);
                    var ResultTrack = await _TrackRepository.Add(track);
                    return MapResultToAddTrackReturnDTO(ResultTrack);
                }
                throw new NoSuchStationFoundException();
            }
            catch (TrackAlreadyAddedException taae)
            {
                throw new TrackAlreadyAddedException();
            }
            catch (NoSuchStationFoundException taae)
            {
                throw new NoSuchStationFoundException();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        // End - Function to add Track to a station by Admin


        // Start - Function to Update PricePerKm of a Train by Admin
        public async Task<UpdatePricePerKmReturnDTO> UpdatePricePerKmbyAdmin(UpdatePricePerKmDTO updatePricePerKmDTO)
        {
            try
            {
                var train = await _TrainRepository.GetbyKey(updatePricePerKmDTO.TrainId);
                if(train != null) {
                    train.PricePerKM = updatePricePerKmDTO.PricePerKm;
                    var UpdatedTrain = await _TrainRepository.Update(train);
                    UpdatePricePerKmReturnDTO updatePricePerKmReturnDTO = new UpdatePricePerKmReturnDTO();
                    updatePricePerKmReturnDTO.PricePerKm = UpdatedTrain.PricePerKM;
                    updatePricePerKmReturnDTO.TrainId = UpdatedTrain.TrainId;
                    return updatePricePerKmReturnDTO;
                }
                throw new NoSuchTrainFoundException();
            }
            catch (NoSuchTrainFoundException)
            {
                throw new NoSuchTrainFoundException();
            }
        }

        // End - Function to Update PricePerKm of a Train by Admin

        // End - Function to Get All the Reserved Seats of a Trains by Admin
        public async Task<> CheckReservedSeatsbyAdmin(int TrainId)
        {
        }
    }
}
