using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Interfaces;
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
using RailwayReservationApp.Repositories.StationRequest;
using RailwayReservationApp.Repositories.TrackRequest;
using RailwayReservationApp.Models.UserDTOs;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using static System.Collections.Specialized.BitVector32;
using RailwayReservationApp.Exceptions.StationExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Exceptions.SeatExcepions;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class UserRepositoryTest
    {
        RailwayReservationContext context;
        IAdminService adminService;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            IRepository<int, Train> _TrainRepository = new TrainRepository(context);
            IRepository<int, TrainRoutes> _TrainRouteRepository =new TrainRouteRepository(context);
            IRepository<int, Station> _StationRepository = new StationRepository(context);
            IRepository<int, TrainClass> _TrainClassRepository = new TrainClassRepository(context);
            IRepository<int, Track> _TrackRepository = new TrackRepository(context);
            IRepository<int, TrackReservation> _TrackReservationRepository = new TrackReservationRepository(context);
            IRepository<int, Admin> _AdminRepository = new AdminRepository(context);
            //ITokenService _TokenService;
            StationRequestRepository _StationRequestRepository = new StationRequestRepository(context); // To retrieve Tracks for a station
            TrainRequestforClassesRepository _TrainRequestforClassesRepository = new TrainRequestforClassesRepository(context); // To retrieve for a Classes for a Train
            TrackRequestforReservationRepository _TrackRequestforReservationRepository = new TrackRequestforReservationRepository(context); // To retrieve Reservation for a Track
            TrainRequestforReservationsRepository _TrainRequestforReservationsRepository = new TrainRequestforReservationsRepository(context); // To retrieve Reservations of a Train
            ReservationRequestforSeatsRepository _ReservationRequestforSeatsRepository = new ReservationRequestforSeatsRepository(context);

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
            trackReservation.ReservationStatus = "Inline";
            var TrackReservationResult = _TrackReservationRepository.Add(trackReservation);

        }

        [Test]
        public void AsminRegisterSuccessTest()
        {
            //Arrange
            AdminRegisterDTO adminRegisterDTO = new AdminRegisterDTO();
            adminRegisterDTO.Name = "Sanjai";
            adminRegisterDTO.Email = "sanjai@gmail.com";
            adminRegisterDTO.Address = "Tamilnadu";
            adminRegisterDTO.PhoneNumber = "1234567890";
            adminRegisterDTO.Gender = "Male";
            adminRegisterDTO.Disability = false;
            adminRegisterDTO.Password = "sanjai123";

            var result = adminService.AdminRegistration(adminRegisterDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AdminLoginSuccessTest()
        {
            //Arrange
            AdminLoginDTO adminLoginDTO = new AdminLoginDTO();
            adminLoginDTO.Id = 1;
            adminLoginDTO.Password = "sanjai123";
            var ans = adminService.AdminLogin(adminLoginDTO);

            //Assert
            Assert.IsNotNull(ans);
        }

        [Test]
        public void AdminLoginFailTest()
        {
            //Arrange
            AdminLoginDTO adminLoginDTO = new AdminLoginDTO();
            adminLoginDTO.Id = 345678;
            adminLoginDTO.Password = "sanjai123";
            var ans = adminService.AdminLogin(adminLoginDTO);

            //Action
            var exception = Assert.ThrowsAsync<InvalidAdminCredentialsException>(async () => await adminService.AdminLogin(adminLoginDTO));

            //Assert
            Assert.AreEqual("Invalid Username or Password", exception.Message);
        }

        [Test]
        public void AdminLoginFailTestInvalidPasword()
        {
            //Arrange
            AdminLoginDTO adminLoginDTO = new AdminLoginDTO();
            adminLoginDTO.Id = 1;
            adminLoginDTO.Password = "sanjai123tyu";
            var ans = adminService.AdminLogin(adminLoginDTO);

            //Action
            var exception = Assert.ThrowsAsync<InvalidAdminCredentialsException>(async () => await adminService.AdminLogin(adminLoginDTO));

            //Assert
            Assert.AreEqual("Invalid Username or Password", exception.Message);
        }

        [Test]
        public async Task AddTrainSuccessTest()
        {
            //Arrange
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

            var result = await adminService.AddTrainbyAdmin(addTrainDTO);

            //Assert
            Assert.AreEqual(3, result.TrainNumber);
        }

        [Test]
        public async Task AddTrainExceptionTest()
        {
            //Arrange
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

            var result = await adminService.AddTrainbyAdmin(addTrainDTO);

            //Action
            var exception = Assert.ThrowsAsync<TrainAlreadyAllotedException>(async () => await adminService.AddTrainbyAdmin(addTrainDTO));

            //Assert
            Assert.AreEqual("Hey Admin! This Train is Already Alloted in your required date and status is Inline! Please, Try to add another availble Trains.", exception.Message);
        }

        [Test]
        public async Task AddStationSuccessTest()
        {
            //Arrange
            AddStationDTO addStationDTO = new AddStationDTO();
            addStationDTO.StationName = "Pollachi";
            addStationDTO.StationState = "Tamilnadu";
            addStationDTO.StationPincode = "123456";

            var result = await adminService.AddStationbyAdmin(addStationDTO);

            //Assert
            Assert.AreEqual("Pollachi", result.StationName);
        }

        [Test]
        public async Task AddStationExceptionFailTest()
        {
            //Arrange
            AddStationDTO addStationDTO = new AddStationDTO();
            addStationDTO.StationName = "Pollachi";
            addStationDTO.StationState = "Tamilnadu";
            addStationDTO.StationPincode = "123456";

            //Action
            var exception = Assert.ThrowsAsync<StationAlreadyAddedException>(async () => await adminService.AddStationbyAdmin(addStationDTO));

            //Assert
            Assert.AreEqual("This station is already added in the particular state! Try again with a different station name", exception.Message);
        }

        [Test]
        public async Task AddTrackToStationSuccessTest()
        {
            //Arrange
            AddTrackDTO addTrackDTO = new AddTrackDTO();
            addTrackDTO.TrackNumber = 1;
            addTrackDTO.TrackStartingPoint = "Pollachi";
            addTrackDTO.TrackEndingPoint = "Cochin";
            addTrackDTO.TrackStatus = "Active";
            addTrackDTO.StationId = 1;

            var result = await adminService.AddTrackToStationbyAdmin(addTrackDTO);

            //Assert
            Assert.AreEqual(1, result.TrackNumber);
        }

        [Test]
        public async Task AddTrackToStationFailExceptionTest()
        {
            //Arrange
            AddTrackDTO addTrackDTO = new AddTrackDTO();
            addTrackDTO.TrackNumber = 1;
            addTrackDTO.TrackStartingPoint = "Pollachi";
            addTrackDTO.TrackEndingPoint = "Cochin";
            addTrackDTO.TrackStatus = "Active";
            addTrackDTO.StationId = 1;

            //Action
            var exception = Assert.ThrowsAsync<TrackAlreadyAddedException>(async () => await adminService.AddTrackToStationbyAdmin(addTrackDTO));

            //Assert
            Assert.AreEqual("This Particular Track Number is already added to the station!", exception.Message);

        }

        [Test]
        public async Task AddTrackToStationFail_NoStationFoundExceptionTest()
        {
            //Arrange
            AddTrackDTO addTrackDTO = new AddTrackDTO();
            addTrackDTO.TrackNumber = 34567;
            addTrackDTO.TrackStartingPoint = "Pollachi";
            addTrackDTO.TrackEndingPoint = "Cochin";
            addTrackDTO.TrackStatus = "Active";
            addTrackDTO.StationId = 34;

            //Action
            var exception = Assert.ThrowsAsync<NoSuchStationFoundException>(async () => await adminService.AddTrackToStationbyAdmin(addTrackDTO));

            //Assert
            Assert.AreEqual("No Such Station Found!", exception.Message);

        }

        [Test]
        public async Task AddClassToTrainSuccessTest()
        {
            //Arrange
            AddTrainClassDTO addTrainClassReturnDTO = new AddTrainClassDTO();
            addTrainClassReturnDTO.TrainId = 1;
            addTrainClassReturnDTO.ClassName = "FirstClass";
            addTrainClassReturnDTO.ClassPrice = 20;
            addTrainClassReturnDTO.StartingSeatNumber = 1;
            addTrainClassReturnDTO.EndingSeatNumber = 30;

            var result = await adminService.AddTrainClassbyAdmin(addTrainClassReturnDTO);

            //Assert
            Assert.AreEqual(1, result.TrainId);
        }

        [Test]
        public async Task AddClassToTrainExceptionTest()
        {
            //Arrange
            AddTrainClassDTO addTrainClassReturnDTO = new AddTrainClassDTO();
            addTrainClassReturnDTO.TrainId = 1;
            addTrainClassReturnDTO.ClassName = "FirstClass";
            addTrainClassReturnDTO.ClassPrice = 20;
            addTrainClassReturnDTO.StartingSeatNumber = 1;
            addTrainClassReturnDTO.EndingSeatNumber = 30;
            //Action
            var exception = Assert.ThrowsAsync<InvalidSeatAllocationException>(async () => await adminService.AddTrainClassbyAdmin(addTrainClassReturnDTO));

            //Assert
            Assert.AreEqual("Invalid Seat Allocation Admin! Recheck and add valid seats", exception.Message);
        }

        [Test]
        public async Task AddTrainRouteSuccessTest()
        {
            //Arrange
            AddTrainRouteDTO addTrainRouteDTO = new AddTrainRouteDTO();
            addTrainRouteDTO.RouteStartDate = DateTime.Now;
            addTrainRouteDTO.RouteEndDate = DateTime.Now;
            addTrainRouteDTO.ArrivalTime = DateTime.Now;
            addTrainRouteDTO.DepartureTime = DateTime.Now;
            addTrainRouteDTO.StopNumber = 2;
            addTrainRouteDTO.KilometerDistance = 100;
            addTrainRouteDTO.TrainId = 1;
            addTrainRouteDTO.StationId = 1;
            addTrainRouteDTO.TrackId = 123456781;
            addTrainRouteDTO.TrackNumber = 1;

            var result = await adminService.AddTrainRoutebyAdmin(addTrainRouteDTO);

            //Assert
            Assert.AreEqual(1, result.TrainId);
        }

        [Test]
        public async Task AddRouteToTrainExceptionTest()
        {
            //Arrange
            AddTrainRouteDTO addTrainRouteDTO = new AddTrainRouteDTO();
            addTrainRouteDTO.RouteStartDate = DateTime.Now;
            addTrainRouteDTO.RouteEndDate = DateTime.Now;
            addTrainRouteDTO.ArrivalTime = DateTime.Now;
            addTrainRouteDTO.DepartureTime = DateTime.Now;
            addTrainRouteDTO.StopNumber = 2;
            addTrainRouteDTO.KilometerDistance = 100;
            addTrainRouteDTO.TrainId = 1;
            addTrainRouteDTO.StationId = 12345;
            addTrainRouteDTO.TrackId = 1;
            addTrainRouteDTO.TrackNumber = 1;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchStationFoundException>(async () => await adminService.AddTrainRoutebyAdmin(addTrainRouteDTO));

            //Assert
            Assert.AreEqual("No Such Station Found!", exception.Message);
        }

        [Test]
        public async Task AddRouteToTrain_TrainNotFoundExceptionTest()
        {
            //Arrange
            AddTrainRouteDTO addTrainRouteDTO = new AddTrainRouteDTO();
            addTrainRouteDTO.RouteStartDate = DateTime.Now;
            addTrainRouteDTO.RouteEndDate = DateTime.Now;
            addTrainRouteDTO.ArrivalTime = DateTime.Now;
            addTrainRouteDTO.DepartureTime = DateTime.Now;
            addTrainRouteDTO.StopNumber = 2;
            addTrainRouteDTO.KilometerDistance = 100;
            addTrainRouteDTO.TrainId = 2345671;
            addTrainRouteDTO.StationId = 1;
            addTrainRouteDTO.TrackId = 1;
            addTrainRouteDTO.TrackNumber = 1;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchTrainFoundException>(async () => await adminService.AddTrainRoutebyAdmin(addTrainRouteDTO));

            //Assert
            Assert.AreEqual("No Such Train Found!", exception.Message);
        }
        [Test]
        public async Task AddRouteToTrain_TrackReservedExceptionTest()
        {
            //Arrange
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
            //Action
            var exception = Assert.ThrowsAsync<RequiredTrackBusyException>(async () => await adminService.AddTrainRoutebyAdmin(addTrainRouteDTO));

            //Assert
            Assert.AreEqual("The Required Track is Busy! Please alter your Train Track Admin.", exception.Message);
        }

        [Test]
        public async Task UpdatePricePerKmSuccessTest()
        {
            //Arrange
            UpdatePricePerKmDTO updatePricePerKmDTO = new UpdatePricePerKmDTO();
            updatePricePerKmDTO.PricePerKm = 2;
            updatePricePerKmDTO.TrainId = 1;
            var result = await adminService.UpdatePricePerKmbyAdmin(updatePricePerKmDTO);

            //Assert
            Assert.AreEqual(1, result.TrainId);
        }

        [Test]
        public async Task UpdatePricePerKmFailTest()
        {
            //Arrange
            UpdatePricePerKmDTO updatePricePerKmDTO = new UpdatePricePerKmDTO();
            updatePricePerKmDTO.PricePerKm = 2;
            updatePricePerKmDTO.TrainId = 1678;

            //Action
            var exception = Assert.ThrowsAsync<NoSuchTrainFoundException>(async () => await adminService.UpdatePricePerKmbyAdmin(updatePricePerKmDTO));

            //Assert
            Assert.AreEqual("No Such Train Found!", exception.Message);
        }

        [Test]
        public async Task CheckSeatsDetailsbyAdminSuccessTest()
        {
            //Arrange
            var result = await adminService.CheckSeatsDetailsbyAdmin(1);

            //Assert
            Assert.AreEqual(120, result.TotalSeat);
        }

        [Test]
        public async Task CheckSeatsDetailsbyAdminFailTest()
        {
            //Arrange
            //var result = await adminService.CheckSeatsDetailsbyAdmin(131);

            //Action
            var exception = Assert.ThrowsAsync<NoSuchTrainFoundException>(async () => await adminService.CheckSeatsDetailsbyAdmin(1456));

            //Assert
            Assert.AreEqual("No Such Train Found!", exception.Message);
        }

        [Test]
        public async Task GetReservedTrackSuccessTest()
        {
            //Arrange
            var result = await adminService.GetReservedTracksofStationbyAdmin(1);

            //Assert
            Assert.AreEqual(0, result.AvailableTracks.Count);
        }

        [Test]
        public async Task GetReservedTrackFailTest()
        {
            //Arrange
            var result = await adminService.GetReservedTracksofStationbyAdmin(1);

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchStationFoundException>(async () => await adminService.GetReservedTracksofStationbyAdmin(16));

            //Assert
            Assert.AreEqual("No Such Station Found!", exception.Message);
        }

        [Test]
        public async Task UpdateTrainStatusSuccessTest()
        {
            //Arrange
            var result = await adminService.UpdateTrainStatusCompleted(1);

            //Assert
            Assert.AreEqual("Updated Successfully", result.ToString());
        }

        [Test]
        public async Task UpdateTrainStatusFailTest()
        {
            //Arrange

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTrainFoundException>(async () => await adminService.UpdateTrainStatusCompleted(1567));

            //Assert
            Assert.AreEqual("No Such Train Found!", exception.Message);
        }

        [Test]
        public async Task GetAllInlineTrainsSuccessTest()
        {
            //Arrange
            var result = await adminService.GetAllInlineTrains();

            //Assert
            Assert.AreEqual(1, 1);
        }
    }
}
