using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;
using RailwayReservationApp.Repositories.ReservationRequest;
using RailwayReservationApp.Repositories.TrainRequest;

namespace RailwayReservationApp.Services
{
    public class UserSecondaryServices : IUserSecondaryService
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
        //private readonly RailwayReservationContext _context;
        public UserSecondaryServices(IRepository<int, Reservation> ReservationRepository, IRepository<int, Train> TrainRepository,
            TrainRequestforTrainRoutesRepository TrainRequestforTrainRoutesRepository, IRepository<int, Station> StationRepository,
            IRepository<int, Seat> SeatRepository, ReservationRequestforSeatsRepository ReservationRequestforSeatsRepository,
            IRepository<int, Users> UserRepository, IRepository<int, UserDetails> UserDetailsRepository, TrainRequestforReservationsRepository TrainRequestforReservationsRepository,
            IRepository<int, Payment> PaymentRepository, IRepository<int, Rewards> RewardRepository, IRepository<int, ReservationCancel> ReservationCancelRepository, ITokenService tokenService,
            TrainRequestforClassesRepository TrainRequestforClassesRepository, IRepository<int, TrainClass> TrainClassRepository, RailwayReservationContext context)
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
            //_context = context;
        }

        public UserSecondaryServices(IRepository<int, Reservation> reservationRepository, IRepository<int, Train> trainRepository, IRepository<int, Station> stationRepository, TrainRequestforTrainRoutesRepository trainRequestforTrainRoutesRepository, IRepository<int, Seat> seatRepository, IRepository<int, Users> userRepository, IRepository<int, UserDetails> userDetailsRepository, IRepository<int, Payment> paymentRepository, IRepository<int, Rewards> rewardRepository, IRepository<int, ReservationCancel> reservationCancelRepository, TrainRequestforReservationsRepository trainRequestforReservationsRepository, ReservationRequestforSeatsRepository reservationRequestforSeatsRepository)
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

        // Start - Function to Get Booked Trains

        public async Task<IList<UserBookedTrainsReturnDTO>> GetBookedTrains(int UserId)
        {
            List<UserBookedTrainsReturnDTO> TrainList = new List<UserBookedTrainsReturnDTO>();
            try
            {
                var Reservations = (await _ReservationRepository.Get()).Where(t => t.Status == "Confirmed").ToList();
                foreach(var reservation in Reservations)
                {
                    if(reservation.UserId == UserId)
                    {
                        var Train = await _TrainRepository.GetbyKey(reservation.TrainId);
                        if(Train.TrainStatus == "Inline")
                        {
                            UserBookedTrainsReturnDTO userBookedTrainsReturnDTO = new UserBookedTrainsReturnDTO();
                            userBookedTrainsReturnDTO.TrainName = Train.TrainName;
                            userBookedTrainsReturnDTO.TrainNumber = Train.TrainNumber;
                            userBookedTrainsReturnDTO.StartingPoint = reservation.StartingPoint;
                            userBookedTrainsReturnDTO.EndingPoint = reservation.EndingPoint;
                            userBookedTrainsReturnDTO.TrainDate = reservation.TrainDate;
                            TrainList.Add(userBookedTrainsReturnDTO);
                        }
                    }
                }
                if(TrainList.Count > 0)
                {
                    return TrainList;
                }
                throw new NoBookedTrainsAvailableException();
            }
            catch (NoBookedTrainsAvailableException)
            {
                throw new NoBookedTrainsAvailableException();
            }
            catch (NoSuchTrainFoundException)
            {
                throw new NoSuchTrainFoundException();
            }
        }

        // End - Function to Get Booked Trains

        // End - Function to Get Past Bookings 

        public async Task<IList<UserBookedTrainsReturnDTO>> GetPastBookings(int UserId)
        {
            List<UserBookedTrainsReturnDTO> TrainList = new List<UserBookedTrainsReturnDTO>();
            try
            {
                var Reservations = (await _ReservationRepository.Get()).Where(t => t.Status == "Confirmed");
                foreach (var reservation in Reservations)
                {
                    if (reservation.UserId == UserId)
                    {
                        var Train = await _TrainRepository.GetbyKey(reservation.TrainId);
                        if (Train.TrainStatus == "Completed")
                        {
                            UserBookedTrainsReturnDTO userBookedTrainsReturnDTO = new UserBookedTrainsReturnDTO();
                            userBookedTrainsReturnDTO.TrainName = Train.TrainName;
                            userBookedTrainsReturnDTO.TrainNumber = Train.TrainNumber;
                            userBookedTrainsReturnDTO.StartingPoint = reservation.StartingPoint;
                            userBookedTrainsReturnDTO.EndingPoint = reservation.EndingPoint;
                            userBookedTrainsReturnDTO.TrainDate = reservation.TrainDate;
                            TrainList.Add(userBookedTrainsReturnDTO);
                        }
                    }
                }
                if (TrainList.Count > 0)
                {
                    return TrainList;
                }
                throw new NoBookedTrainsAvailableException();
            }
            catch (NoBookedTrainsAvailableException)
            {
                throw new NoBookedTrainsAvailableException();
            }
            catch (NoSuchTrainFoundException)
            {
                throw new NoSuchTrainFoundException();
            }
            catch (NoReservationsFoundException)
            {
                throw new NoReservationsFoundException();
            }
        }

        // End - Function to Get Past Bookings 

        // Start - Function to Update User profile

        public Users MapUserUpdateDTOtoUser(UpdateUserDTO updateUserDTO)
        {
            Users user = new Users();
            user.Id = updateUserDTO.Id;
            user.Name = updateUserDTO.Name;
            user.Address = updateUserDTO.Address;
            user.PhoneNumber = updateUserDTO.PhoneNumber;
            user.Email = updateUserDTO.Email;
            user.Disability = updateUserDTO.Disability;
            user.Gender = updateUserDTO.Gender;
            return user;
        }

        public async Task<Users> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            try
            {
                Users user = MapUserUpdateDTOtoUser(updateUserDTO);
                var IsUserAvailable = await _UserRepository.GetbyKey(user.Id);
                if (IsUserAvailable != null)
                {
                    var UpdatesResult = await _UserRepository.Update(user);
                    if (UpdatesResult != null)
                    {
                        return UpdatesResult;
                    }
                }
                throw new NoSuchUserFoundException();
            }
            catch (NoSuchUserFoundException)
            {
                throw new NoSuchUserFoundException();
            }
        }

         // End - Function to Update User profile


        // Start - Function to (Soft)Delete User profile

        public async Task<string> DeleteUser(int UserId)
        {
            try
            {
                var UserDetail = await _UserDetailsRepository.GetbyKey(UserId);
                if(UserDetail == null)
                {
                    throw new NoSuchUserFoundException();
                }
                UserDetail.Status = "InActive";
                var Result = await _UserDetailsRepository.Update(UserDetail);
                if(Result != null)
                {
                    return "Deleted Successfully";
                }
                throw new NoSuchUserFoundException();
            }
            catch (NoSuchUserFoundException)
            {
                throw new NoSuchUserFoundException();
            }
        }

        // End - Function to (Soft)Delete User profile

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
            try
            {
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

    }
}
