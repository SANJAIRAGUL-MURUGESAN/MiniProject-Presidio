using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Exceptions.RewardExceptions;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;
using RailwayReservationApp.Repositories.ReservationRequest;
using RailwayReservationApp.Repositories.StationRequest;
using RailwayReservationApp.Repositories.TrainRequest;
using System.Security.Cryptography;
using System.Text;

namespace RailwayReservationApp.Services
{
    public class UserServices : IUserService
    {
        private IRepository<int, Reservation> _ReservationRepository;
        private IRepository<int, Train> _TrainRepository;
        private TrainRequestforTrainRoutesRepository _TrainRequestforTrainRoutesRepository; // to Retrieve Routes of a Train
        private IRepository<int, Station> _StationRepository;
        private IRepository<int, Seat> _SeatRepository;
        private ReservationRequestforSeatsRepository _ReservationRequestforSeatsRepository;
        private IRepository<int, Users> _UserRepository;
        private IRepository<int, UserDetails> _UserDetailsRepository;
        private TrainRequestforReservationsRepository _TrainRequestforReservationsRepository;
        private IRepository<int, Payment> _PaymentRepository;
        private IRepository<int, Rewards> _RewardRepository;
        private IRepository<int, ReservationCancel> _ReservationCancelRepository;

        public UserServices(IRepository<int, Reservation> ReservationRepository, IRepository<int, Train> TrainRepository,
            TrainRequestforTrainRoutesRepository TrainRequestforTrainRoutesRepository, IRepository<int, Station> StationRepository,
            IRepository<int, Seat> SeatRepository, ReservationRequestforSeatsRepository ReservationRequestforSeatsRepository,
            IRepository<int, Users> UserRepository, IRepository<int, UserDetails> UserDetailsRepository, TrainRequestforReservationsRepository TrainRequestforReservationsRepository,
            IRepository<int, Payment> PaymentRepository, IRepository<int, Rewards> RewardRepository,IRepository<int, ReservationCancel> ReservationCancelRepository) 
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

        //public async Task RevertUserInsert(UserDetails userDetails)
        //{
        //    await _UserDetailsRepository.Update(userDetails.UserDetailsId);
        //}
        public async Task<UserRegisterReturnDTO> UserRegistration(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                Users user = MapUserRegisterDTOtoUser(userRegisterDTO);
                var UserAdditionResult = await _UserRepository.Add(user);
                user.Id = UserAdditionResult.Id;
                UserDetails userDetails = MapUserAdditionResultToAddUserDeatails(userRegisterDTO, user);
                var UserDetailsAdditionResult = await _UserDetailsRepository.Add(userDetails);
                if( UserAdditionResult != null && UserAdditionResult == null)
                {
                    await RevertUserdetailsInsert(UserAdditionResult);
                    throw new UnableToRegisterException();
                }
                //if( UserAdditionResult == null && UserAdditionResult != null)
                //{
                //    await RevertUserInsert(UserDetailsAdditionResult);
                //}
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


        // Start - Function to Search a Train by User (By Starting point, Ending Point, Start Date)

        public async Task<IEnumerable<Train>> SearchTrainByUser(SearchTrainDTO searchTrainDTO)
        {
            try
            {
                var Trains = await _TrainRepository.Get(); // Getting All Trains
                // Fetching whether Train is available as per User Requirement(End to End Starting Point)
                var InlineTrains = Trains.Where(t => t.TrainStartDate.Date == searchTrainDTO.TrainStartDate.Date &&
                t.StartingPoint == searchTrainDTO.StartingPoint && t.EndingPoint == searchTrainDTO.EndingPoint && t.TrainStatus == "Inline").ToList();
                if (InlineTrains.Count > 0)
                {
                    return InlineTrains;
                }
                // Fetching whether Train is available as per User Requirement(Using Train Routes)
                // Fetching Available Trains based on Start date and Status == "Inline"
                var FutureTrains = Trains.Where(t => t.TrainStatus == "Inline").ToList();
                foreach (var train in FutureTrains) // Traversing those Trains to get train routes
                {
                    // Getting Train Routes for Every Train that are available
                    var TrainRoutes = await _TrainRequestforTrainRoutesRepository.GetbyKey(train.TrainId);
                    List<TrainRoutes> TrainRouteDetails = TrainRoutes.TrainRoutes.ToList(); // Convertig to List
                    int StartingPointFlag = 0, EndingPointFlag = 0;
                    foreach(var Route in TrainRouteDetails)
                    {
                        var station = await _StationRepository.GetbyKey(Route.StationId);
                        Console.WriteLine("Station name "+station.StationName);
                        if((station.StationName == searchTrainDTO.StartingPoint) && (Route.RouteStartDate.Date == searchTrainDTO.TrainStartDate.Date) && StartingPointFlag == 0)
                        {
                            StartingPointFlag = 1;
                            continue;
                        }
                        if((station.StationName == searchTrainDTO.EndingPoint) && StartingPointFlag == 1)
                        {
                            EndingPointFlag = 1;
                            break;
                        }
                    }
                    if(StartingPointFlag == 1 && EndingPointFlag == 1)
                    {
                        InlineTrains.Add(train);
                    }
                }
                if(InlineTrains != null)
                {
                    return InlineTrains;
                }
                throw new NoTrainsAvailableforyourSearchException();
            }
            catch (NoTrainsFoundException nstde)
            {
                throw new NoTrainsFoundException();
            }
            catch(Exception ex)
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
                if(train == null)
                {
                    throw new NoSuchTrainFoundException();
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
                    return StartingPointkm * train.PricePerKM;
                }
                int Flag = 0;
                float TotalKilometer = 0;
                foreach (var route in Routes)
                {
                    var station = await _StationRepository.GetbyKey(route.StationId);
                    if (station.StationName == bookTrainDTO.StartingPoint)
                    {
                        Flag = 1;
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
                return TotalKilometer * trainRoutes.PricePerKM * bookTrainDTO.Seats.Count;
            }catch(NoSuchTrainFoundException nsfe)
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
                foreach(var seats in bookTrainDTO.Seats)
                {
                    foreach(var reservation in ReservationDetails)
                    {
                        var SeatDetails = await _ReservationRequestforSeatsRepository.GetbyKey(reservation.ReservationId);
                        List<Seat> SeatAloneDetails = SeatDetails.Seats.ToList();
                        foreach (var seat in SeatAloneDetails)
                        {
                            if(seat.SeatNumber == seats)
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
                if (Reservation.Amount > 3000)
                {
                    Rewards rewards = new Rewards();
                    rewards.UserId = Reservation.UserId;
                    rewards.RewardPoints = 10;
                    var RewardAdditionResult = await _RewardRepository.Add(rewards);
                }
                Reservation.Status = "Confirmed";
                var UpdatedReservation = await _ReservationRepository.Update(Reservation);
                return MapPaymentAdditionToAddPaymentReturnDTO(payment);
            }catch(NoSuchReservationFoundException)
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
                Reservation.Status = "Cancelled";
                // Updating the Reservation Model
                var UpdatedReservation = await _ReservationRepository.Update(Reservation);
                // Cancelling Rewards if Reward amount is greater than 3000
                if (Reservation.Amount > 3000)
                {
                    var Rewards = await _RewardRepository.Get();
                    // Random Deletion if any reward exits
                    if (Rewards != null)
                    {
                        foreach (var reward in Rewards)
                        {
                            var RewardDeletion = await _RewardRepository.Delete(reward.RewardId);
                            break;
                        }

                    }
                }
                // Fetching the Reservation Seats to delete the Reserved Seats
                var ReservationSeats = await _ReservationRequestforSeatsRepository.GetbyKey(Reservation.ReservationId);
                var ReservedSeats = ReservationSeats.Seats.ToList();
                foreach (var seat in ReservedSeats)
                {
                    // Checking Seats in Reservation Seats and Deleting
                    if (seat.SeatNumber == cancelReservationDTO.SeatNumber)
                    {
                        var DeletedSeat = await _SeatRepository.Delete(seat.SeatId);
                    }
                }
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
            catch(NoSuchSeatFoundException)
            {
                throw new NoSuchSeatFoundException();
            }
            catch (Exception)
            {
                throw new Exception();

            }
            
        }
        // End - Function to Cancel Reservation


    }
}
