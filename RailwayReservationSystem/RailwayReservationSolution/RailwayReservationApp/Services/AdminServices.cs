using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Exceptions.StationExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Exceptions.TrainClassExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Exceptions.TrainRoutesExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;
using RailwayReservationApp.Repositories;
using RailwayReservationApp.Repositories.ReservationRequest;
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
        private readonly IRepository<int, Admin> _AdminRepository;
        private readonly IRepository<int, Refund> _RefundRepository;
        private readonly ITokenService _TokenService;
        private readonly StationRequestRepository _StationRequestRepository; // To retrieve Tracks for a station
        private readonly TrainRequestforClassesRepository _TrainRequestforClassesRepository; // To retrieve for a Classes for a Train
        private readonly TrackRequestforReservationRepository _TrackRequestforReservationRepository; // To retrieve Reservation for a Track
        private readonly TrainRequestforReservationsRepository _TrainRequestforReservationsRepository; // To retrieve Reservations of a Train
        private readonly ReservationRequestforSeatsRepository _ReservationRequestforSeatsRepository; // To retrieve Seats of a Reservation
        //private IRepository<int, Train> repository1;
        //private TrainRepository trainRepository;
        //private IRepository<int, TrainRoutes> repository2;
        //private TrainRouteRepository trainRouteRepository;
        //private IRepository<int, Station> repository3;
        //private StationRepository stationRepository;
        //private StationRequestRepository stationRequestRepository1;
        //private StationRequestRepository stationRequestRepository2;
        //private TrainRequestforClassesRepository trainRequestforClassesRepository1;
        //private TrainRequestforClassesRepository trainRequestforClassesRepository2;
        //private IRepository<int, Track> repository4;
        //private TrackRepository trackRepository;
        //private TrackRequestforReservationRepository trackRequestforReservationRepository1;
        //private TrackRequestforReservationRepository trackRequestforReservationRepository2;
        //private IRepository<int, TrackReservation> repository5;
        //private TrackReservationRepository trackReservationRepository;
        //private IRepository<int, TrainClass> repository6;
        //private TrainClassRepository trainClassRepository;
        //private TrainRequestforReservationsRepository trainRequestforReservationsRepository1;
        //private TrainRequestforReservationsRepository trainRequestforReservationsRepository2;
        //private ReservationRequestforSeatsRepository reservationRequestforSeatsRepository;
        //private object reservationRequestforSeatsrepository;
        //private IRepository<int, Admin> repository7;
        //private AdminRepository adminRepository;
        //private IRepository<int, Train> trainRepository1;
        //private IRepository<int, TrainRoutes> trainRouteRepository1;
        //private IRepository<int, Station> stationRepository1;
        //private IRepository<int, TrainClass> trainClassRepository1;
        //private IRepository<int, Track> trackRepository1;
        //private IRepository<int, TrackReservation> trackReservationRepository1;
        //private IRepository<int, Admin> adminRepository1;
        //private StationRequestRepository stationRequestRepository;
        //private TrainRequestforClassesRepository trainRequestforClassesRepository;
        //private TrackRequestforReservationRepository trackRequestforReservationRepository;
        //private TrainRequestforReservationsRepository trainRequestforReservationsRepository;

        public AdminServices(IRepository<int, Train> trainRepository, TrainRepository trainRepository1, IRepository<int, TrainRoutes> trainRouteRepository, IRepository<int, Station> stationRepository,
            StationRequestRepository stationRequestRepository, TrainRequestforClassesRepository trainRequestforClassesRepository,
            IRepository<int, Track> trackRepository, TrackRequestforReservationRepository trackRequestforReservationRepository,
            IRepository<int, TrackReservation> trackReservationRepository, IRepository<int, TrainClass> trainClassRepository,
            TrainRequestforReservationsRepository TrainRequestforReservationsRepository, ReservationRequestforSeatsRepository ReservationRequestforSeatsRepository,
            IRepository<int, Admin> AdminRepository, ITokenService tokenService,IRepository<int, Refund> refundRepository)
        {
            _TrainRepository = trainRepository;
            _TrainRouteRepository = trainRouteRepository;
            _StationRepository = stationRepository;
            _StationRequestRepository = stationRequestRepository;
            _TrainRequestforClassesRepository = trainRequestforClassesRepository;
            _TrackRepository = trackRepository;
            _TrackReservationRepository = trackReservationRepository;
            _TrackRequestforReservationRepository = trackRequestforReservationRepository;
            _TrainClassRepository = trainClassRepository;
            _TrainRequestforReservationsRepository = TrainRequestforReservationsRepository;
            _ReservationRequestforSeatsRepository = ReservationRequestforSeatsRepository;
            _AdminRepository = AdminRepository;
            _TokenService = tokenService;
            _RefundRepository = refundRepository;
        }

        public AdminServices(IRepository<int, Train> trainRepository1, IRepository<int, TrainRoutes> trainRouteRepository1, IRepository<int, Station> stationRepository1, IRepository<int, TrainClass> trainClassRepository1, IRepository<int, Track> trackRepository1, IRepository<int, TrackReservation> trackReservationRepository1, IRepository<int, Admin> adminRepository1, StationRequestRepository stationRequestRepository, TrainRequestforClassesRepository trainRequestforClassesRepository, TrackRequestforReservationRepository trackRequestforReservationRepository, TrainRequestforReservationsRepository trainRequestforReservationsRepository, ReservationRequestforSeatsRepository reservationRequestforSeatsRepository)
        {
            this._TrainRepository = trainRepository1;
            this._TrainRouteRepository = trainRouteRepository1;
            this._StationRepository = stationRepository1;
            this._TrainClassRepository = trainClassRepository1;
            this._TrackRepository = trackRepository1;
            this._TrackReservationRepository = trackReservationRepository1;
            this._AdminRepository = adminRepository1;
            this._StationRequestRepository = stationRequestRepository;
            this._TrainRequestforClassesRepository = trainRequestforClassesRepository;
            this._TrackRequestforReservationRepository = trackRequestforReservationRepository;
            this._TrainRequestforReservationsRepository = trainRequestforReservationsRepository;
            this._ReservationRequestforSeatsRepository = reservationRequestforSeatsRepository;
        }

        // Start - Function to Start Admin Registration

        public Admin MapAdminRegisterDTOtoAdmin(AdminRegisterDTO adminRegisterDTO)
        {
            Admin admin = new Admin();
            admin.Name = adminRegisterDTO.Name;
            admin.Address = adminRegisterDTO.Address;
            admin.Email = adminRegisterDTO.Email;
            admin.Gender = adminRegisterDTO.Gender;
            admin.Disability = adminRegisterDTO.Disability;
            admin.Password = adminRegisterDTO.Password;
            admin.PhoneNumber = adminRegisterDTO.PhoneNumber;
            return admin;
        }

        public AdminRegisterReturnDTO AdminAdditionToAdminRegisterReturnDTO(Admin admin)
        {
            AdminRegisterReturnDTO adminRegisterReturnDTO = new AdminRegisterReturnDTO();
            adminRegisterReturnDTO.Name = admin.Name;
            adminRegisterReturnDTO.Address = admin.Address;
            adminRegisterReturnDTO.PhoneNumber = admin.PhoneNumber;
            adminRegisterReturnDTO.Email = admin.Email;
            adminRegisterReturnDTO.Gender = admin.Gender;
            adminRegisterReturnDTO.Disability = admin.Disability;
            return adminRegisterReturnDTO;
        }
        public async Task<AdminRegisterReturnDTO> AdminRegistration(AdminRegisterDTO adminRegisterDTO)
        {
            try
            {
                Admin admin = MapAdminRegisterDTOtoAdmin(adminRegisterDTO);
                var AdminAddition = await _AdminRepository.Add(admin);
                return AdminAdditionToAdminRegisterReturnDTO(AdminAddition);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // End - Function to Start Admin Registration

        // Start - Function to Admin Login
        private bool ComparePassword(char[] dbPassword, char[] DTOpassword)
        {
            for (int i = 0; i < dbPassword.Length; i++)
            {
                if (dbPassword[i] != DTOpassword[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<AdminLoginReturnDTO> MapUserTOLoginReturnDTO(Admin admin)
        {
            AdminLoginReturnDTO adminLoginReturnDTO = new AdminLoginReturnDTO();
            adminLoginReturnDTO.UserId = admin.Id;
            adminLoginReturnDTO.Token = await _TokenService.GenerateTokenAdmin(admin);
            adminLoginReturnDTO.Role = "Admin";
            return adminLoginReturnDTO;
        }
        public async Task<AdminLoginReturnDTO> AdminLogin(AdminLoginDTO adminLoginDTO)
        {
            try
            {
                // Function to Check whether Admin Id is availble in DB 
                var IsAdminAvailable = await _AdminRepository.GetbyKey(adminLoginDTO.Id);
                if(IsAdminAvailable != null)
                {
                    bool isPasswordSame = ComparePassword((IsAdminAvailable.Password).ToArray(), (adminLoginDTO.Password).ToArray());
                    if (isPasswordSame)
                    {
                        AdminLoginReturnDTO adminLoginReturnDTO = await MapUserTOLoginReturnDTO(IsAdminAvailable);
                        return adminLoginReturnDTO;
                    }
                    throw new InvalidAdminCredentialsException();
                }
                throw new InvalidAdminCredentialsException();
            }
            catch (InvalidAdminCredentialsException)
            {
                throw new InvalidAdminCredentialsException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
            // End - Function to Admin Login

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
                        if (addTrainDTO.TrainStartDate.Date >= train.TrainStartDate.Date && addTrainDTO.TrainStartDate.Date <= train.TrainEndDate.Date)
                        {
                            throw new TrainAlreadyAllotedException();
                        }
                        if (addTrainDTO.TrainEndDate.Date >= train.TrainStartDate.Date && addTrainDTO.TrainEndDate.Date <= train.TrainEndDate.Date)
                        {
                            throw new TrainAlreadyAllotedException();
                        }
                        if(addTrainDTO.TrainStartDate.Date <= train.TrainStartDate.Date && addTrainDTO.TrainEndDate.Date >= train.TrainEndDate.Date)
                        {
                            throw new TrainAlreadyAllotedException();
                        }
                    }
                }
                Train trains = MapAddTrainDTOtoTrain(addTrainDTO);
                var result = await _TrainRepository.Add(trains);
                return MapResultToAddTrainReturnDTO(result);
            }
            catch (TrainAlreadyAllotedException taae)
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
            foreach (var Reservation in trackReservation)
            {
                if (addTrainRouteDTO.RouteStartDate >= Reservation.TrackOccupiedStartDate && addTrainRouteDTO.RouteStartDate <= Reservation.TrackOccupiedEndDate)
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
            trackReservation.TrainId = trainRoutes.TrainId;
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
                if (station == null)
                {
                    throw new NoSuchStationFoundException();
                }
                if (train != null)
                {
                    // Fetching the tracks for the station Provided by Admin
                    var StationTracks = await _StationRequestRepository.GetbyKey(addTrainRouteDTO.StationId);
                    // Fetching the tracks that are available (Already Track will be added by Admin)
                    List<Track> TrackList = StationTracks.Tracks.ToList();
                    Console.WriteLine("TrackCount :" + TrackList.Count);
                    if (TrackList.Count > 0)
                    {
                        bool ReservationResult = true; // If identify desired track is available or not 
                        foreach (var Track in TrackList)
                        {
                            if (Track.TrackNumber == addTrainRouteDTO.TrackNumber)
                            {
                                var TrackReservation = await _TrackRequestforReservationRepository.GetbyKey(addTrainRouteDTO.TrackId);
                                ReservationResult = CheckReservationAvailable(TrackReservation.TrackReservations.Where(r => r.ReservationStatus == "Inline").ToList(), addTrainRouteDTO);
                                if (ReservationResult == true)
                                {
                                    TrainRoutes trainRoutes = MapAddTrainRouteDTOtoTrainRoute(addTrainRouteDTO);
                                    var RoutedAddedResult = await _TrainRouteRepository.Add(trainRoutes);
                                    var TrackReservationAddResult = TrackReservationAddition(trainRoutes);
                                    return MapResulttoAddTrainRouteReturnDTO(RoutedAddedResult);
                                }
                            }
                        }
                        if (ReservationResult == false)
                        {
                            throw new RequiredTrackBusyException();
                        }
                    }
                    throw new NoTrainTracksFoundException();
                }
                throw new NoSuchTrainFoundException();
            }
            catch (NoSuchTrainFoundException nsfe)
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
                if (train != null)
                {
                    // Fetching Classes(if available) for a Train
                    var TrainResult = await _TrainRequestforClassesRepository.GetbyKey(addTrainClassDTO.TrainId);
                    List<TrainClass> trainClasses = TrainResult.TrainClasses.ToList();
                    int Flag = 0;
                    if (trainClasses.Count > 0)
                    {
                        foreach (var classes in trainClasses)
                        {
                            if(classes.ClassName == addTrainClassDTO.ClassName)
                            {
                                throw new ClassAlreadyAddedException();
                            }
                            if (addTrainClassDTO.StartingSeatNumber >= classes.StartingSeatNumber && addTrainClassDTO.StartingSeatNumber <= classes.EndingSeatNumber)
                            {
                                Flag = 1;
                                break;
                            }
                            if (addTrainClassDTO.EndingSeatNumber >= classes.StartingSeatNumber && addTrainClassDTO.EndingSeatNumber <= classes.EndingSeatNumber)
                            {
                                Flag = 1;
                                break;
                            }
                            if (addTrainClassDTO.StartingSeatNumber > train.TotalSeats || addTrainClassDTO.EndingSeatNumber > train.TotalSeats)
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
            catch (NoSuchTrainClassFoundException)
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
                foreach (var station in stations)
                {
                    if (station.StationName == addStationDTO.StationName)
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
            catch (Exception ex)
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
                if (train != null) {
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

        // Start - Function to Get All the Reserved Seats of a Trains by Admin

        public CheckSeatDetailsReturnDTO MapCheckDetailstoReturnDTO(List<int> ReservedSeats, List<int> AvailableSeats, int TotalSeats)
        {
            CheckSeatDetailsReturnDTO checkSeatDetailsReturnDTO = new CheckSeatDetailsReturnDTO();
            checkSeatDetailsReturnDTO.TotalSeat = TotalSeats;
            checkSeatDetailsReturnDTO.ReservedSeats = ReservedSeats;
            checkSeatDetailsReturnDTO.AvailableSeats = AvailableSeats;
            return checkSeatDetailsReturnDTO;
        }
        public async Task<CheckSeatDetailsReturnDTO> CheckSeatsDetailsbyAdmin(int TrainId)
        {
            try {
                // Checking Train is Available or not in DB
                var train = await _TrainRepository.GetbyKey(TrainId);
                if (train == null)
                {
                    throw new NoSuchTrainFoundException();
                }
                List<int> ReservedSeats = new List<int>();
                List<int> AvailableSeats = new List<int>();
                // Fetching Reservation of a Train
                var ReservationDetails = await _TrainRequestforReservationsRepository.GetbyKey(TrainId);
                // Fetching Reservation Alone from Reservation Details
                List<Reservation> reservations = ReservationDetails.TrainReservations.ToList();
                // Traversing reservations List to get Seats
                foreach (var reservation in reservations)
                {
                    // Fetching Seats of every reservation
                    var SeatDetails = await _ReservationRequestforSeatsRepository.GetbyKey(reservation.ReservationId);
                    // Fetching Seats alone from SeatDetails
                    List<Seat> seats = SeatDetails.Seats.ToList();
                    if (seats.Count > 0)
                    {
                        // Traversing to Get Seat Number
                        foreach (var seat in seats)
                        {
                            ReservedSeats.Add(seat.SeatNumber);
                        }
                    }
                }
                // Traversing Reserved Seats list to get Unresered Seats
                for (int i = 1; i < train.TotalSeats; i++)
                {
                    if (!ReservedSeats.Contains(i))
                    {
                        AvailableSeats.Add(i);
                    }
                }
                return MapCheckDetailstoReturnDTO(ReservedSeats, AvailableSeats, train.TotalSeats);
            }
            catch (NoSuchTrainFoundException)
            {
                throw new NoSuchTrainFoundException();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        // End - Function to Get All the Reserved Seats of a Trains by Admin

        // Start - Function to Get the Reserved Tracks of a Station
        public async Task<GetReservedTracksReturnDTO> GetReservedTracksofStationbyAdmin(int StationId)
        {
            try
            {
                List<int> ReservedTracks = new List<int>();
                List<int> AvailableTracks = new List<int>();
                // Function to check a Station is Availale
                var IsStationAvailable = await _StationRepository.GetbyKey(StationId);
                if (IsStationAvailable != null)
                {
                    // Function to Get the Tracks of a Station
                    var TrackDetails = await _StationRequestRepository.GetbyKey(StationId);
                    var TrackAloneDetails = TrackDetails.Tracks.ToList();
                    if (TrackAloneDetails.Count > 0)
                    {
                        // Traversing Track to get Track Reservation Details
                        foreach (var track in TrackAloneDetails)
                        {
                            // Fetching Reservation Details
                            var TrackReservationDetails = await _TrackRequestforReservationRepository.GetbyKey(track.TrackId);
                            var TrackReservations = TrackReservationDetails.TrackReservations.ToList();
                            // Traversing TrackReservations to get status link "Inline, Completed"
                            foreach (var reservation in TrackReservations)
                            {
                                // Chacking the Status
                                if (reservation.ReservationStatus == "Inline")
                                {
                                    ReservedTracks.Add((await _TrackRepository.GetbyKey(reservation.TrackId)).TrackNumber);
                                }
                            }
                        }
                        for (int i = 0; i < TrackAloneDetails.Count; i++)
                        {
                            if (!ReservedTracks.Contains(TrackAloneDetails[i].TrackNumber))
                            {
                                AvailableTracks.Add(TrackAloneDetails[i].TrackNumber);
                            }
                        }
                        // Mapping
                        GetReservedTracksReturnDTO getReservedTracksReturnDTO = new GetReservedTracksReturnDTO();
                        getReservedTracksReturnDTO.ReservedTracks = ReservedTracks;
                        getReservedTracksReturnDTO.AvailableTracks = AvailableTracks;
                        return getReservedTracksReturnDTO;
                    }
                    throw new NoTrainTracksFoundException();
                }
                throw new NoSuchStationFoundException();
            }
            catch (NoTrainTracksFoundException)
            {
                throw new NoTrainTracksFoundException();
            }
            catch (NoSuchStationFoundException)
            {
                throw new NoSuchStationFoundException();
            }
        }

        // End - Function to Get the Reserved Tracks of a Station

        // Start - Function to Update the Train Status as Completed
        public async Task<string> UpdateTrainStatusCompleted(int TrainId)
        {
            try
            {
                // Function to check Train is Available or Not
                var IsTrainAvailable = await _TrainRepository.GetbyKey(TrainId);
                if (IsTrainAvailable != null)
                {
                    IsTrainAvailable.TrainStatus = "Completed";
                    // Updating
                    var UpdatedResult = await _TrainRepository.Update(IsTrainAvailable);
                    var ReservedTracks = await _TrackReservationRepository.Get();
                    foreach(var reservatoin in ReservedTracks)
                    {
                        if(reservatoin.TrainId == TrainId)
                        {
                            reservatoin.ReservationStatus = "Completed";
                            await _TrackReservationRepository.Update(reservatoin);
                        }
                    }
                    return "Updated Successfully";
                }
                throw new NoSuchTrainFoundException();
            }
            catch (NoSuchTrainFoundException)
            {
                throw new NoSuchTrainFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // End - Function to Update the Train Status as Completed

        // Start - Function to Update the Train Route Details

        public TrainRoutes MapUpdateTrainRouteDTOtoTrainRoutes(UpdateTrainRouteDetailsDTO updateTrainRouteDetailsDTO)
        {
            TrainRoutes trainRoutes = new TrainRoutes();
            trainRoutes.RouteId = updateTrainRouteDetailsDTO.RouteId;
            trainRoutes.TrainId = updateTrainRouteDetailsDTO.TrainId;
            trainRoutes.RouteStartDate = updateTrainRouteDetailsDTO.RouteStartDate;
            trainRoutes.RouteEndDate = updateTrainRouteDetailsDTO.RouteEndDate;
            trainRoutes.ArrivalTime = updateTrainRouteDetailsDTO.ArrivalTime;
            trainRoutes.DepartureTime = updateTrainRouteDetailsDTO.DepartureTime;
            trainRoutes.TrackId = updateTrainRouteDetailsDTO.TrackId;
            trainRoutes.TrackNumber = updateTrainRouteDetailsDTO.TrackNumber;
            trainRoutes.KilometerDistance = updateTrainRouteDetailsDTO.KilometerDistance;
            trainRoutes.StopNumber = updateTrainRouteDetailsDTO.StopNumber;
            trainRoutes.StationId = updateTrainRouteDetailsDTO.StationId;
            return trainRoutes;
        }

        public async Task<TrainRoutes> UpdateTrainRouteDetails(UpdateTrainRouteDetailsDTO updateTrainRouteDetailsDTO)
        {
            try
            {
                TrainRoutes trainRoutes = MapUpdateTrainRouteDTOtoTrainRoutes(updateTrainRouteDetailsDTO);
                // Updating
                var UpdatedTrainRoutes = await _TrainRouteRepository.Update(trainRoutes);
                return UpdatedTrainRoutes;
                throw new NoSuchTrainRouteFoundException();
            }
            catch (NoSuchTrainRouteFoundException)
            {
                throw new NoSuchTrainRouteFoundException();
            }
        }

        // End - Function to Update the Train Route Details

        // Start - Function to Get the All Inline Trains
        public async Task<IList<Train>> GetAllInlineTrains()
        {
            try
            {
                var InlineTrains = (await _TrainRepository.Get()).Where(t => t.TrainStatus == "Inline").ToList();
                if (InlineTrains.Count > 0)
                {
                    return InlineTrains.ToList();
                }
                throw new NoTrainsFoundException();
            }
            catch (NoTrainsFoundException)
            {
                throw new NoTrainsFoundException();
            }
            catch (Exception )
            {
                throw new Exception();
            }
        }

        // End - Function to Get the All Inline Trains
       public Refund MapAddRefundDTOtoRefund(AddRefundDTO addRefundDTO)
        {
            Refund refund = new Refund();
            float RefundAmount = addRefundDTO.RefundAmount * (50 / 100);
            refund.RefundDate = addRefundDTO.RefundDate;
            refund.ReservationCancelId = addRefundDTO.ReservationCancelId;
            refund.UserId = addRefundDTO.UserId;
            refund.RefundAmount = RefundAmount;
            return refund;
        }

        public AddRefundReturnDTO MapRefundToAddRefundReturnDTO(Refund refund)
        {
            AddRefundReturnDTO addRefundReturnDTO = new AddRefundReturnDTO();
            addRefundReturnDTO.UserId = refund.UserId;
            addRefundReturnDTO.ReservationCancelId = refund.ReservationCancelId;
            addRefundReturnDTO.RefundAmount = refund.RefundAmount;
            addRefundReturnDTO.RefundDate = refund.RefundDate;
            return addRefundReturnDTO;
        }
        public async Task<AddRefundReturnDTO> ProcessRefundByAdmin(AddRefundDTO addRefundDTO)
        {
            try
            {
                Refund refund = MapAddRefundDTOtoRefund(addRefundDTO);
                var RefundResult = await _RefundRepository.Add(refund);
                return MapRefundToAddRefundReturnDTO(RefundResult);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
