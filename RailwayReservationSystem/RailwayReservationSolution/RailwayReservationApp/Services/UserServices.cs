using Microsoft.AspNetCore.Routing;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Exceptions.RewardExceptions;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Exceptions.TrainClassExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;
using RailwayReservationApp.Repositories;
using RailwayReservationApp.Repositories.ReservationRequest;
using RailwayReservationApp.Repositories.StationRequest;
using RailwayReservationApp.Repositories.TrainRequest;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace RailwayReservationApp.Services
{ 
    public class UserServices : IUserService
    {
        private readonly IRepository<int, Reservation> _ReservationRepository;
        private readonly IRepository<int, Train> _TrainRepository;
        private readonly TrainRequestforTrainRoutesRepository _TrainRequestforTrainRoutesRepository; // to Retrieve Routes of a Train
        private readonly IRepository<int, Station> _StationRepository;
        private readonly IRepository<int, Seat> _SeatRepository;
        private readonly ReservationRequestforSeatsRepository _ReservationRequestforSeatsRepository;
        private readonly IRepository<int, Users> _UserRepository;
        private readonly IRepository<int, UserDetails> _UserDetailsRepository;
        private readonly TrainRequestforReservationsRepository _TrainRequestforReservationsRepository;
        private readonly IRepository<int, Payment> _PaymentRepository;
        private readonly IRepository<int, Rewards> _RewardRepository;
        private readonly IRepository<int, ReservationCancel> _ReservationCancelRepository;
        private readonly ITokenService _TokenService;
        private readonly IRepository<int, TrainClass> _TrainClassRepository;
        private readonly TrainRequestforClassesRepository _TrainRequestforClassesRepository;

        public UserServices(IRepository<int, Reservation> ReservationRepository, IRepository<int, Train> TrainRepository,
            TrainRequestforTrainRoutesRepository TrainRequestforTrainRoutesRepository, IRepository<int, Station> StationRepository,
            IRepository<int, Seat> SeatRepository, ReservationRequestforSeatsRepository ReservationRequestforSeatsRepository,
            IRepository<int, Users> UserRepository, IRepository<int, UserDetails> UserDetailsRepository, TrainRequestforReservationsRepository TrainRequestforReservationsRepository,
            IRepository<int, Payment> PaymentRepository, IRepository<int, Rewards> RewardRepository, IRepository<int, ReservationCancel> ReservationCancelRepository, ITokenService tokenService,
            TrainRequestforClassesRepository TrainRequestforClassesRepository, IRepository<int, TrainClass> TrainClassRepository)
        {
            _ReservationRepository = ReservationRepository;
            _TrainRepository = TrainRepository;
            _TrainRequestforTrainRoutesRepository = TrainRequestforTrainRoutesRepository;
            _StationRepository = StationRepository;
            _SeatRepository = SeatRepository;
            _ReservationRequestforSeatsRepository = ReservationRequestforSeatsRepository;
            _UserRepository = UserRepository;
            _UserDetailsRepository = UserDetailsRepository;
            _TrainRequestforReservationsRepository = TrainRequestforReservationsRepository;
            _PaymentRepository = PaymentRepository;
            _RewardRepository = RewardRepository;
            _ReservationCancelRepository = ReservationCancelRepository;
            _TokenService = tokenService;
            _TrainRequestforClassesRepository = TrainRequestforClassesRepository;
            _TrainClassRepository = TrainClassRepository;
        }

        public UserServices(IRepository<int, Reservation> reservationRepository, IRepository<int, Train> trainRepository, IRepository<int, Station> stationRepository, TrainRequestforTrainRoutesRepository trainRequestforTrainRoutesRepository, IRepository<int, Seat> seatRepository, IRepository<int, Users> userRepository, IRepository<int, UserDetails> userDetailsRepository, IRepository<int, Payment> paymentRepository, IRepository<int, Rewards> rewardRepository, IRepository<int, ReservationCancel> reservationCancelRepository, TrainRequestforReservationsRepository trainRequestforReservationsRepository, ReservationRequestforSeatsRepository reservationRequestforSeatsRepository)
        {
            this._ReservationRepository = reservationRepository;
            this._TrainRepository = trainRepository;
            this._StationRepository = stationRepository;
            this._TrainRequestforTrainRoutesRepository = trainRequestforTrainRoutesRepository;
            this._SeatRepository = seatRepository;
            this._UserRepository = userRepository;
            this._UserDetailsRepository = userDetailsRepository;
            this._PaymentRepository = paymentRepository;
            this._RewardRepository = rewardRepository;
            this._ReservationCancelRepository = reservationCancelRepository;
            this._TrainRequestforReservationsRepository = trainRequestforReservationsRepository;
            this._ReservationRequestforSeatsRepository = reservationRequestforSeatsRepository;
            //this._TrainRequestforClassesRepository = trainRequestforClassesRepository;
        }


        // Start - Function for User Registration

        public Users MapUserRegisterDTOtoUser(UserRegisterDTO userRegisterDTO)
        {
            Users user = new Users();
            user.Name = userRegisterDTO.Name;
            user.Email = userRegisterDTO.Email;
            user.Gender = userRegisterDTO.Gender;
            user.Address = userRegisterDTO.Address;
            user.PhoneNumber = userRegisterDTO.PhoneNumber;
            user.Disability = userRegisterDTO.Disability;
            return user;
        }
        public UserDetails MapUserAdditionResultToAddUserDeatails(UserRegisterDTO users, Users user)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.UserId = user.Id;
            HMACSHA512 hMACSHA = new HMACSHA512();
            userDetails.PasswordHashKey = hMACSHA.Key;
            userDetails.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(users.Password));
            userDetails.Status = "Active";
            return userDetails;
        }

        public UserRegisterReturnDTO UserAdditionResultToUserRegisterReturnDTO(Users users)
        {
            UserRegisterReturnDTO userRegisterReturnDTO = new UserRegisterReturnDTO();
            userRegisterReturnDTO.Name = users.Name;
            userRegisterReturnDTO.Email = users.Email;
            userRegisterReturnDTO.Gender = users.Gender;
            userRegisterReturnDTO.PhoneNumber = users.PhoneNumber;
            userRegisterReturnDTO.Address = users.Address;
            userRegisterReturnDTO.Disability = users.Disability;
            return userRegisterReturnDTO;
        }
        private async Task RevertUserdetailsInsert(Users user)
        {
            await _UserRepository.Delete(user.Id);
        }

        public async Task RevertUserInsert(UserDetails userDetails)
        {
            await _UserDetailsRepository.Update(userDetails);
        }
        public async Task<UserRegisterReturnDTO> UserRegistration(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                Users user = MapUserRegisterDTOtoUser(userRegisterDTO);
                var UserAdditionResult = await _UserRepository.Add(user);
                user.Id = UserAdditionResult.Id;
                UserDetails userDetails = MapUserAdditionResultToAddUserDeatails(userRegisterDTO, user);
                var UserDetailsAdditionResult = await _UserDetailsRepository.Add(userDetails);
                if (UserAdditionResult != null && UserAdditionResult == null)
                {
                    await RevertUserdetailsInsert(UserAdditionResult);
                    throw new UnableToRegisterException();
                }
                if (UserAdditionResult == null && UserAdditionResult != null)
                {
                    await RevertUserInsert(UserDetailsAdditionResult);
                }
                return UserAdditionResultToUserRegisterReturnDTO(user);
            }
            catch (NoSuchUserFoundException)
            {
                throw new NoSuchUserFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // End - Function for User Registration


        // Start - Function for User Login

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }

        private UserLoginReturnDTO MapEmployeeToLoginReturn(Users user)
        {
            UserLoginReturnDTO returnDTO = new UserLoginReturnDTO();
            returnDTO.UserId = user.Id;
            returnDTO.Token = _TokenService.GenerateToken(user);
            returnDTO.Role = "User";
            return returnDTO;
        }
        public async Task<UserLoginReturnDTO> Login(UserLoginDTO loginDTO)
        {
            var userDB = await _UserDetailsRepository.GetbyKey(loginDTO.UserId);
            if (userDB == null)
            {
                throw new InvalidCredentialsException();
            }
            HMACSHA512 hMACSHA = new HMACSHA512(userDB.PasswordHashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, userDB.Password);
            if (isPasswordSame)
            {
                var user = await _UserRepository.GetbyKey(loginDTO.UserId);
                UserLoginReturnDTO loginReturnDTO = MapEmployeeToLoginReturn(user);
                return loginReturnDTO;
            }
            throw new InvalidCredentialsException();
        }
        // End - Function for User Login


        // Start - Function to Search a Train by User (By Starting point, Ending Point, Start Date)

        public async Task<bool> RouteFunction(int TrainId, string StartingPoint, string EndingPoint, DateTime StartDate)
        {
            var TrainDetails = await _TrainRequestforTrainRoutesRepository.GetbyKey(TrainId);
            List<TrainRoutes> TrainAloneDetails = TrainDetails.TrainRoutes.ToList();
            int StartingPointFlag = 0;
            int EndingPointFlag = 0;
            if(TrainDetails.StartingPoint == StartingPoint && TrainDetails.TrainStartDate.Date == StartDate.Date)
            {
                foreach (var route in TrainAloneDetails)
                {
                    var station = await _StationRepository.GetbyKey(route.StationId);
                    Console.WriteLine("Station name " + station.StationName);
                    if ((station.StationName == EndingPoint))
                    {
                        return true;
                    }
                }
                return false;
            }
            foreach (var route in TrainAloneDetails)
            {
                var station = await _StationRepository.GetbyKey(route.StationId);
                Console.WriteLine("Station name " + station.StationName);
                if ((station.StationName == StartingPoint) && (route.RouteEndDate.Date == StartDate.Date))
                {
                    StartingPointFlag = 1;
                    continue;
                }
                if ((station.StationName == EndingPoint) && StartingPointFlag == 1)
                {
                    EndingPointFlag = 1;
                    break;
                }
            }
            if (StartingPointFlag == 1 && EndingPointFlag == 1)
            {
                return true;
            }
            return false;
        }
        public async Task<IList<TrainSearchResultDTO>> SearchTrainByUser(SearchTrainDTO searchTrainDTO)
        {
            try
            {
                List<TrainSearchResultDTO> Result = new List<TrainSearchResultDTO>();
                var Trains = (await _TrainRepository.Get()).Where(t => t.StartingPoint == searchTrainDTO.StartingPoint && t.EndingPoint == searchTrainDTO.EndingPoint && t.TrainStartDate.Date == searchTrainDTO.TrainStartDate.Date && t.TrainStatus == "Inline").ToList(); // Getting All Trains
                if (Trains.Count> 0)
                {
                    foreach (var Train in Trains)
                    {
                        TrainSearchResultDTO trainSearchResultDTO = new TrainSearchResultDTO();
                        trainSearchResultDTO.TrainName = Train.TrainName;
                        trainSearchResultDTO.TrainNumber = Train.TrainNumber;
                        trainSearchResultDTO.StartingPoint = Train.StartingPoint;
                        trainSearchResultDTO.EndingPoint = Train.EndingPoint;
                        trainSearchResultDTO.TrainCapacity = Train.TotalSeats;
                        trainSearchResultDTO.StartDate = Train.TrainStartDate;
                        trainSearchResultDTO.EndDate = Train.TrainEndDate;
                        Result.Add(trainSearchResultDTO);
                    }
                    return Result;
                }
                // Fetching whether Train is available as per User Requirement(Using Train Routes)
                // Fetching Available Trains based on Start date and Status == "Inline"
                List<int> TrainID = new List<int>();
                List<Train> FutureTrains = ((await _TrainRepository.Get()).Where(t => t.TrainStatus == "Inline")).ToList();
                foreach (var train in FutureTrains)
                {
                    TrainID.Add(train.TrainId);
                }
                foreach (int i in TrainID)
                {
                    bool IsTrue = await RouteFunction(i, searchTrainDTO.StartingPoint, searchTrainDTO.EndingPoint, searchTrainDTO.TrainStartDate);
                    if (IsTrue == true)
                    {
                        var Train = await _TrainRepository.GetbyKey(i);
                        TrainSearchResultDTO trainSearchResultDTO = new TrainSearchResultDTO();
                        trainSearchResultDTO.TrainName = Train.TrainName;
                        trainSearchResultDTO.TrainNumber = Train.TrainNumber;
                        trainSearchResultDTO.StartingPoint = Train.StartingPoint;
                        trainSearchResultDTO.EndingPoint = Train.EndingPoint;
                        trainSearchResultDTO.TrainCapacity = Train.TotalSeats;
                        trainSearchResultDTO.StartDate = Train.TrainStartDate;
                        trainSearchResultDTO.EndDate = Train.TrainEndDate;
                        Result.Add(trainSearchResultDTO);
                    }
                }
                if (Result.Count > 0)
                {
                    return Result;
                }
                throw new NoTrainsAvailableforyourSearchException();
            }
            catch (NoTrainsFoundException nstde)
            {
                throw new NoTrainsFoundException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // End - Function to Search a Train by User

        // Start - Function to Book a Train by User

        public async Task<Train> IsTrainExists(int TrainId) // To check whether Train is Exists(Added by Admin)
        {
            try
            {
                var train = await _TrainRepository.GetbyKey(TrainId);
                if (train != null)
                {
                    return train;
                }
                throw new NoSuchTrainFoundException();
            }
            catch (NoSuchTrainFoundException)
            {
                throw new NoSuchTrainFoundException();
            }
        }

        public async Task<float> CalculateTravelPrice(BookTrainDTO bookTrainDTO)
        {
            try
            {
                var train = await _TrainRepository.GetbyKey(bookTrainDTO.TrainId);
                var trainRoutes = await _TrainRequestforTrainRoutesRepository.GetbyKey(bookTrainDTO.TrainId);
                if (train == null)
                {
                    throw new NoSuchTrainFoundException();
                }
                var ClassDetails = await _TrainRequestforClassesRepository.GetbyKey(bookTrainDTO.TrainId);
                var ClassAloneDetails = ClassDetails.TrainClasses.ToList();
                float ClassPrice = 1;
                foreach (var classes in ClassAloneDetails)
                {
                    if (classes.ClassName == bookTrainDTO.ClassName)
                    {
                        ClassPrice = (await _TrainClassRepository.GetbyKey(classes.ClassId)).ClassPrice;
                        break;
                    }
                }
                List<TrainRoutes> Routes = trainRoutes.TrainRoutes.ToList();
                if (bookTrainDTO.StartingPoint == train.StartingPoint)
                {
                    float StartingPointkm = 0;
                    foreach (var route in Routes)
                    {
                        var station = await _StationRepository.GetbyKey(route.StationId);
                        StartingPointkm = StartingPointkm + route.KilometerDistance;
                        if (station.StationName == bookTrainDTO.EndingPoint)
                        {
                            break;
                        }
                    }
                    return StartingPointkm * train.PricePerKM * bookTrainDTO.Seats.Count + (bookTrainDTO.Seats.Count * ClassPrice);
                }
                int Flag = 0;
                float TotalKilometer = 0;
                foreach (var route in Routes)
                {
                    var station = await _StationRepository.GetbyKey(route.StationId);
                    if (station.StationName == bookTrainDTO.StartingPoint)
                    {
                        Flag = 1;
                        continue;
                    }
                    if (Flag == 1)
                    {
                        TotalKilometer = TotalKilometer + route.KilometerDistance;
                    }
                    if (station.StationName == bookTrainDTO.EndingPoint)
                    {
                        Flag = 0;
                    }
                }
                return TotalKilometer * trainRoutes.PricePerKM * bookTrainDTO.Seats.Count + (bookTrainDTO.Seats.Count * ClassPrice);
            } 
            catch (NoSuchTrainFoundException nsfe)
            {
                throw new NoSuchTrainFoundException();
            }
        }

        public Reservation MapBookTrainDTOtoReservation(BookTrainDTO bookTrainDTO, float TravelPrice)
        {
            Reservation reservation = new Reservation();
            reservation.ReservationDate = DateTime.Now;
            reservation.StartingPoint = bookTrainDTO.StartingPoint;
            reservation.EndingPoint = bookTrainDTO.EndingPoint;
            reservation.TrainDate = bookTrainDTO.TrainDate;
            reservation.Status = "PaymentBending";
            reservation.TrainId = bookTrainDTO.TrainId;
            reservation.UserId = bookTrainDTO.UserId;
            reservation.Amount = TravelPrice;
            reservation.TrainClassName = bookTrainDTO.ClassName;
            return reservation;
        }

        public BookTrainReturnDTO MapResultToBookTrainReturnDTO(Reservation reservation)
        {
            BookTrainReturnDTO bookTrainReturnDTO = new BookTrainReturnDTO();
            bookTrainReturnDTO.ReservationDate = reservation.ReservationDate;
            bookTrainReturnDTO.TrainDate = reservation.TrainDate;
            bookTrainReturnDTO.Amount = reservation.Amount;
            bookTrainReturnDTO.StartingPoint = reservation.StartingPoint;
            bookTrainReturnDTO.EndingPoint = reservation.EndingPoint;
            bookTrainReturnDTO.Status = reservation.Status;
            bookTrainReturnDTO.TrainId = reservation.TrainId;
            bookTrainReturnDTO.UserId = reservation.UserId;
            bookTrainReturnDTO.ClassName = reservation.TrainClassName;
            //bookTrainReturnDTO.Seats = (List<int>)reservation.Seats;
            return bookTrainReturnDTO;
        }

        public async void RevertReservationAddition(int ReservationId)
        {
            await _ReservationRepository.Delete(ReservationId);
        }


        public async Task<BookTrainReturnDTO> BookTrainByUser(BookTrainDTO bookTrainDTO)
        {
            try
            {
                // Checking whether is Train Exists
                var IsTrainAvailable = await IsTrainExists(bookTrainDTO.TrainId);
                // Calculating Total Price for the Travel
                float TravelPrice = await CalculateTravelPrice(bookTrainDTO);
                Reservation Reservation = MapBookTrainDTOtoReservation(bookTrainDTO, TravelPrice);
                var ReservationAdditionResult = await _ReservationRepository.Add(Reservation);
                var TrainReservationDetails = await _TrainRequestforReservationsRepository.GetbyKey(bookTrainDTO.TrainId);
                var ReservationDetails = TrainReservationDetails.TrainReservations.ToList();
                foreach (var seats in bookTrainDTO.Seats)
                {
                    foreach (var reservation in ReservationDetails)
                    {
                        var SeatDetails = await _ReservationRequestforSeatsRepository.GetbyKey(reservation.ReservationId);
                        List<Seat> SeatAloneDetails = SeatDetails.Seats.ToList();
                        foreach (var seat in SeatAloneDetails)
                        {
                            if (seat.SeatNumber == seats)
                            {
                                RevertReservationAddition(ReservationAdditionResult.ReservationId);
                                throw new SeatAlreadyReservedException();
                            }
                        }
                    }
                    if (seats <= IsTrainAvailable.TotalSeats)
                    {
                        Seat seat = new Seat();
                        seat.ReservationId = ReservationAdditionResult.ReservationId;
                        seat.SeatNumber = seats;
                        var ResultSeat = await _SeatRepository.Add(seat);
                    }
                }
                return MapResultToBookTrainReturnDTO(ReservationAdditionResult);
            }
            catch (NoSuchTrainFoundException)
            {
                throw new NoSuchTrainFoundException();
            }
        }

        // End - Function to Search and Book a Train by User

        // Start - Function to Confirm Pending

        public Payment MapAddpaymentDTOtoPayment(AddPaymentDTO addPaymentDTO)
        {
            Payment payment = new Payment();
            payment.PaymentMethod = addPaymentDTO.PaymentMethod;
            payment.PaymentDate = addPaymentDTO.PaymentDate;
            payment.Amount = addPaymentDTO.Amount;
            payment.Status = addPaymentDTO.Status;
            payment.ReservationId = addPaymentDTO.ReservationId;
            return payment;
        }

        public AddPaymentReturnDTO MapPaymentAdditionToAddPaymentReturnDTO(Payment payment)
        {
            AddPaymentReturnDTO addPaymentReturnDTO = new AddPaymentReturnDTO();
            addPaymentReturnDTO.PaymentMethod = payment.PaymentMethod;
            addPaymentReturnDTO.Status = payment.Status;
            addPaymentReturnDTO.Amount = payment.Amount;
            addPaymentReturnDTO.PaymentDate = payment.PaymentDate;
            return addPaymentReturnDTO;
        }
        public async Task<AddPaymentReturnDTO> ConfirmPayment(AddPaymentDTO addPaymentDTO)
        {
            try
            {
                Payment payment = MapAddpaymentDTOtoPayment(addPaymentDTO);
                var Payment = await _PaymentRepository.Add(payment);
                var Reservation = await _ReservationRepository.GetbyKey(Payment.ReservationId);
                if (addPaymentDTO.Amount > 3000)
                {
                    Rewards rewards = new Rewards();
                    // Is Reward Already Available
                    var IsRewardAvailable = (await _RewardRepository.Get()).Where(r => r.UserId == Reservation.UserId).ToList();
                    if (IsRewardAvailable.Count == 0)
                    {
                        rewards.UserId = Reservation.UserId;
                        rewards.RewardPoints = 10;
                        var RewardAdditionResult = await _RewardRepository.Add(rewards);
                    }
                    else
                    {
                        IsRewardAvailable[0].RewardPoints = IsRewardAvailable[0].RewardPoints + 10;
                        var RewardAdditionResult = await _RewardRepository.Update(IsRewardAvailable[0]);
                    }
                }
                Reservation.Status = "Confirmed";
                var UpdatedReservation = await _ReservationRepository.Update(Reservation);
                return MapPaymentAdditionToAddPaymentReturnDTO(payment);
            } catch (NoSuchReservationFoundException)
            {
                throw new NoSuchReservationFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // End - Function to Confirm Payment Pending


        // Start - Function to Cancel Reservation

        public ReservationCancel MapCancelReservationDTOtoCancelReservation(CancelReservationDTO cancelReservationDTO)
        {
            ReservationCancel reservationCancel = new ReservationCancel();
            reservationCancel.ReservationCancelReason = cancelReservationDTO.ReservationCancelReason;
            reservationCancel.ReservationId = cancelReservationDTO.ReservationId;
            reservationCancel.SeatNumber = cancelReservationDTO.SeatNumber;
            reservationCancel.UserId = cancelReservationDTO.UserId;
            reservationCancel.RefundAmount = cancelReservationDTO.RefundAmount;
            return reservationCancel;
        }

        public CancelReservationReturnDTO MapCancelReservationTOCancelReservationReturnDTO(ReservationCancel cancelReservation)
        {
            CancelReservationReturnDTO cancelReservationReturnDTO = new CancelReservationReturnDTO();
            cancelReservationReturnDTO.ReservationCancelReason = cancelReservation.ReservationCancelReason;
            cancelReservationReturnDTO.SeatNumber = cancelReservation.SeatNumber;
            cancelReservationReturnDTO.RefundAmount = cancelReservation.RefundAmount;
            return cancelReservationReturnDTO;
        }
        public async Task<CancelReservationReturnDTO> CancelReservation(CancelReservationDTO cancelReservationDTO)
        {
            try
            {
                var CancelReservation = MapCancelReservationDTOtoCancelReservation(cancelReservationDTO);
                // Adding ReservationCancel Data in DB
                var CancelAdditionResul = await _ReservationCancelRepository.Add(CancelReservation);
                // Fetching Reservation Detail to Update status as "Cancelled"
                var Reservation = await _ReservationRepository.GetbyKey(CancelReservation.ReservationId);
                // Cancelling Rewards if Reward amount is greater than 3000
                if (Reservation.Amount > 3000)
                {
                    var Rewards = await _RewardRepository.GetbyKey(Reservation.UserId);
                    // Random Deletion if any reward exits
                    if (Rewards != null)
                    {
                        if (Rewards.RewardPoints >= 10)
                        {
                            Rewards.RewardPoints = Rewards.RewardPoints - 10;
                            await _RewardRepository.Update(Rewards);
                        }
                    }
                }
                // Fetching the Reservation Seats to delete the Reserved Seats
                var ReservationSeats = await _ReservationRequestforSeatsRepository.GetbyKey(Reservation.ReservationId);
                var ReservedSeats = ReservationSeats.Seats.ToList();
                string UString = "Deleted Seats : ";
                foreach (var seat in ReservedSeats)
                {
                    UString += $"{seat.SeatNumber}, ";
                    // Checking Seats in Reservation Seats and Deleting
                    if (seat.SeatNumber == cancelReservationDTO.SeatNumber)
                    {
                        var DeletedSeat = await _SeatRepository.Delete(seat.SeatId);
                    }
                }
                Reservation.Status = UString;
                // Updating the Reservation Model
                var UpdatedReservation = await _ReservationRepository.Update(Reservation);
                return MapCancelReservationTOCancelReservationReturnDTO(CancelReservation);
            }
            catch (NoSuchReservationFoundException)
            {
                throw new NoSuchReservationFoundException();
            }
            catch (NoSuchRewardFoundException)
            {
                throw new NoSuchRewardFoundException();
            }
            catch (NoSuchSeatFoundException)
            {
                throw new NoSuchSeatFoundException();
            }
            catch (Exception)
            {
                throw new Exception();

            }

        }
        // End - Function to Cancel Reservation

        // Start - Function to Get Classes of a Train
        public async Task<IList<GetTrainClassReturnDTO>> GetAllClassofTrain(int  TrainId)
        {
            try
            {
                var IsTrainAvailable = await _TrainRepository.GetbyKey(TrainId);
                if (IsTrainAvailable != null)
                {
                    List<GetTrainClassReturnDTO> TrainClassess = new List<GetTrainClassReturnDTO>();
                    var TrainClassDetails = await _TrainRequestforClassesRepository.GetbyKey(TrainId);
                    var TrainAloneDetails = TrainClassDetails.TrainClasses.ToList();
                    foreach (var classes in TrainAloneDetails)
                    {
                        GetTrainClassReturnDTO getTrainClassReturn = new GetTrainClassReturnDTO();
                        getTrainClassReturn.ClassName = classes.ClassName;
                        getTrainClassReturn.ClassPrice = classes.ClassPrice;
                        getTrainClassReturn.StartingSeatNumber = classes.StartingSeatNumber;
                        getTrainClassReturn.EndingSeatNumber = classes.EndingSeatNumber;
                        TrainClassess.Add(getTrainClassReturn);
                    }
                    if (TrainClassess.Count > 0)
                    {
                        return TrainClassess;
                    }
                    throw new NoTrainClassFoundException();
                }
                throw new NoSuchTrainFoundException();
            }
            catch (NoTrainClassFoundException)
            {
                throw new NoTrainClassFoundException();
            }
            catch (NoSuchTrainFoundException)
            {
                throw new NoSuchTrainFoundException();
            }
        }
        // End - Function to Get Classes of a Train

    }
}
