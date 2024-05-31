using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models.UserDTOs;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories.ReservationRequest;
using RailwayReservationApp.Repositories.TrainRequest;
using RailwayReservationApp.Repositories;
using RailwayReservationApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Repositories.StationRequest;
using RailwayReservationApp.Repositories.TrackRequest;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Exceptions.TrainExceptions;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class UserSecondaryServiceTest
    {
        RailwayReservationContext context;
        IUserSecondaryService userService;

        IAdminService adminService;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);
            IRepository<int, TrainRoutes> _TrainRouteRepository = new TrainRouteRepository(context);
            IRepository<int, TrainClass> _TrainClassRepository = new TrainClassRepository(context);
            IRepository<int, Track> _TrackRepository = new TrackRepository(context);
            IRepository<int, Admin> _AdminRepository = new AdminRepository(context);
            StationRequestRepository _StationRequestRepository = new StationRequestRepository(context);
            IRepository<int, TrackReservation> _TrackReservationRepository = new TrackReservationRepository(context);
            TrackRequestforReservationRepository _TrackRequestforReservationRepository = new TrackRequestforReservationRepository(context);

            IRepository<int, Reservation> _ReservationRepository = new ReservationRepository(context);
            IRepository<int, Train> _TrainRepository = new TrainRepository(context);
            TrainRequestforTrainRoutesRepository _TrainRequestforTrainRoutesRepository = new TrainRequestforTrainRoutesRepository(context);
            IRepository<int, Station> _StationRepository = new StationRepository(context);
            IRepository<int, Seat> _SeatRepository = new SeatRepository(context);
            ReservationRequestforSeatsRepository _ReservationRequestforSeatsRepository = new ReservationRequestforSeatsRepository(context);
            IRepository<int, Users> _UserRepository = new UserRepository(context);
            IRepository<int, UserDetails> _UserDetailsRepository = new UserDetailRepository(context);
            TrainRequestforReservationsRepository _TrainRequestforReservationsRepository = new TrainRequestforReservationsRepository(context);
            IRepository<int, Payment> _PaymentRepository = new PaymentRepository(context);
            IRepository<int, Rewards> _RewardRepository = new RewardRepository(context);
            IRepository<int, ReservationCancel> _ReservationCancelRepository = new ReservationCancelRepository(context);
            TrainRequestforClassesRepository _TrainRequestforClassesRepository = new TrainRequestforClassesRepository(context);
            //Action
            userService = new UserSecondaryServices(_ReservationRepository, _TrainRepository, _StationRepository, _TrainRequestforTrainRoutesRepository,
                _SeatRepository, _UserRepository, _UserDetailsRepository, _PaymentRepository, _RewardRepository, _ReservationCancelRepository,
                _TrainRequestforReservationsRepository, _ReservationRequestforSeatsRepository);

            adminService = new AdminServices(_TrainRepository, _TrainRouteRepository, _StationRepository, _TrainClassRepository,
                _TrackRepository, _TrackReservationRepository, _AdminRepository, _StationRequestRepository, _TrainRequestforClassesRepository,
                _TrackRequestforReservationRepository, _TrainRequestforReservationsRepository, _ReservationRequestforSeatsRepository);

            AddTrainDTO addTrainDTO = new AddTrainDTO();
            addTrainDTO.TrainNumber = 3;
            addTrainDTO.TrainName = "Amirtha Express2";
            addTrainDTO.ArrivalTime = DateTime.Now;
            addTrainDTO.DepartureTime = DateTime.Now;
            addTrainDTO.StartingPoint = "Madurai";
            addTrainDTO.EndingPoint = "Cochin";
            addTrainDTO.PricePerKM = 1;
            addTrainDTO.TotalSeats = 120;
            addTrainDTO.TrainStatus = "Inline";
            addTrainDTO.TrainStartDate = DateTime.Now;
            addTrainDTO.TrainEndDate = DateTime.Now;

            var Trainresult = adminService.AddTrainbyAdmin(addTrainDTO);

            AddStationDTO addStationDTO = new AddStationDTO();
            addStationDTO.StationName = "Pollachi";
            addStationDTO.StationState = "Tamilnadu";
            addStationDTO.StationPincode = "123456";
            var result = adminService.AddStationbyAdmin(addStationDTO);

            AddTrackDTO addTrackDTO = new AddTrackDTO();
            addTrackDTO.TrackNumber = 1;
            addTrackDTO.TrackStartingPoint = "Pollachi";
            addTrackDTO.TrackEndingPoint = "Cochin";
            addTrackDTO.TrackStatus = "Active";
            addTrackDTO.StationId = 1;

            var Trackresult = adminService.AddTrackToStationbyAdmin(addTrackDTO);

            AddTrainClassDTO addTrainClassReturnDTO = new AddTrainClassDTO();
            addTrainClassReturnDTO.TrainId = 1;
            addTrainClassReturnDTO.ClassName = "FirstClass";
            addTrainClassReturnDTO.ClassPrice = 20;
            addTrainClassReturnDTO.StartingSeatNumber = 1;
            addTrainClassReturnDTO.EndingSeatNumber = 30;

            var ClassResult = adminService.AddTrainClassbyAdmin(addTrainClassReturnDTO);

            AddTrainRouteDTO addTrainRouteDTO = new AddTrainRouteDTO();
            addTrainRouteDTO.RouteStartDate = DateTime.Now;
            addTrainRouteDTO.RouteEndDate = DateTime.Now;
            addTrainRouteDTO.ArrivalTime = DateTime.Now;
            addTrainRouteDTO.DepartureTime = DateTime.Now;
            addTrainRouteDTO.StopNumber = 2;
            addTrainRouteDTO.KilometerDistance = 100;
            addTrainRouteDTO.TrainId = 1;
            addTrainRouteDTO.StationId = 1;
            addTrainRouteDTO.TrackId = 1;
            addTrainRouteDTO.TrackNumber = 1;

            var Routeresult = adminService.AddTrainRoutebyAdmin(addTrainRouteDTO);

            TrackReservation trackReservation = new TrackReservation();
            trackReservation.TrackId = 1;
            trackReservation.TrackOccupiedStartTime = DateTime.Now;
            trackReservation.TrackOccupiedEndTime = DateTime.Now;
            trackReservation.TrackOccupiedStartDate = DateTime.Now;
            trackReservation.TrackOccupiedEndDate = DateTime.Now;
            trackReservation.ReservationStatus = "Confirmed";
            var TrackReservationResult = _TrackReservationRepository.Add(trackReservation);

        }

        [Test]
        public void GetBookedTrainSuccessTest()
        {
            //Arrange

            var ans = userService.GetBookedTrains(1);

            //Assert
            Assert.IsNotNull(ans);
        }

        [Test]
        public void GetBookedTrainFailTest()
        {
            //Arrange

            //Action
            var exception = Assert.ThrowsAsync<NoBookedTrainsAvailableException>(async () => await userService.GetBookedTrains(145));

            //Assert
            Assert.AreEqual("Hey User, No Booked Trains Available!", exception.Message);
        }

        [Test]
        public void GetPastBookingSuccessTest()
        {
            //Arrange

            var ans = userService.GetPastBookings(1);

            //Assert
            Assert.IsNotNull(ans);
        }

        [Test]
        public void GetPastBookingFailTest()
        {
            //Arrange

            var ans = userService.GetPastBookings(1);

            ///Action
            var exception = Assert.ThrowsAsync<NoBookedTrainsAvailableException>(async () => await userService.GetPastBookings(234));

            //Assert
            Assert.AreEqual("Hey User, No Booked Trains Available!", exception.Message);
        }

        [Test]
        public void UpdateUserSuccessTest()
        {
            //Arrange
            UpdateUserDTO updateUserDTO = new UpdateUserDTO();
            updateUserDTO.Id = 1;
            updateUserDTO.Name = "Sanjai";
            updateUserDTO.Address = "Tamilnadu";
            updateUserDTO.Gender = "Male";
            updateUserDTO.PhoneNumber = "23456789";
            updateUserDTO.Email = "sanjai@gmail.com";
            updateUserDTO.Disability = false;

            var ans = userService.UpdateUser(updateUserDTO);

            //Assert
            Assert.IsNotNull(ans);
        }

        [Test]
        public void UpdateUserFailTest()
        {
            //Arrange
            UpdateUserDTO updateUserDTO = new UpdateUserDTO();
            updateUserDTO.Id = 1234567;
            updateUserDTO.Name = "Sanjai";
            updateUserDTO.Address = "Tamilnadu";
            updateUserDTO.Gender = "Male";
            updateUserDTO.PhoneNumber = "23456789";
            updateUserDTO.Email = "sanjai@gmail.com";
            updateUserDTO.Disability = false;

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchUserFoundException>(async () => await userService.UpdateUser(updateUserDTO));

            //Assert
            Assert.AreEqual("No Such User Found!", exception.Message);
        }

        [Test]
        public void DeleteUserSuccessTest()
        {
            //Arrange

            var ans = userService.DeleteUser(1);

            //Assert
            Assert.IsNotNull(ans);
        }

        [Test]
        public void DeleteUserFailTest()
        {
            //Arrange

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchUserFoundException>(async () => await userService.DeleteUser(1456));

            //Assert
            Assert.AreEqual("No Such User Found!", exception.Message);
        }

        [Test]
        public void CheckSeatDetailsSuccessTest()
        {
            //Arrange

            var ans = userService.CheckSeatsDetailsbyAdmin(1);

            //Assert
            Assert.IsNotNull(ans);
        }

        [Test]
        public void CheckSeatDetailsFailTest()
        {
            //Arrange

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTrainFoundException>(async () => await userService.CheckSeatsDetailsbyAdmin(13456));

            //Assert
            Assert.AreEqual("No Such Train Found!", exception.Message);
        }

    }
}
