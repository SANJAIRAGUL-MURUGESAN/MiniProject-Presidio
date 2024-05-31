using Microsoft.EntityFrameworkCore;
using Moq;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Controllers;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest
{
    public class AdminControllerTest
    {
        RailwayReservationContext context;
        AdminController adminController;
        private readonly IAdminService adminService;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            adminController = new AdminController(new Mock<IAdminService>().Object);
        }

        [Test]
        public async Task AdminRegisterSuccessTest()
        {
            //Arrange
            AdminRegisterDTO adminRegister = new AdminRegisterDTO();
            adminRegister.Name = "sanjai";
            adminRegister.Address = "Tamilnadu";
            adminRegister.Gender = "Male";
            adminRegister.PhoneNumber = "234567890";
            adminRegister.Disability = false;
            adminRegister.Email = "sanjai@gmail.com";
            adminRegister.Password = "sanjai123";

            var result = await adminController.AdminRegister(adminRegister);

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task AdminLoginSuccessTest()
        {
            //Arrange
            //Arrange
            AdminRegisterDTO adminRegister = new AdminRegisterDTO();
            adminRegister.Name = "sanjai";
            adminRegister.Address = "Tamilnadu";
            adminRegister.Gender = "Male";
            adminRegister.PhoneNumber = "234567890";
            adminRegister.Disability = false;
            adminRegister.Email = "sanjai@gmail.com";
            adminRegister.Password = "sanjai123";

            var Result = await adminController.AdminRegister(adminRegister);
            AdminLoginDTO adminLogin = new AdminLoginDTO();
            adminLogin.Id = 1;
            adminLogin.Password = "sanjai123";

            var result = await adminController.AdminLogin(adminLogin);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AdminAddTrainSuccessTest()
        {
            //Arrange
            AddTrainDTO addTrainDTO = new AddTrainDTO();
            addTrainDTO.TrainNumber = 1;
            addTrainDTO.TrainName = "AmirthaExpress";
            addTrainDTO.TrainStartDate = DateTime.Now;
            addTrainDTO.TrainEndDate = DateTime.Now;
            addTrainDTO.ArrivalTime = DateTime.Now;
            addTrainDTO.DepartureTime = DateTime.Now;
            addTrainDTO.StartingPoint = "Madurai";
            addTrainDTO.EndingPoint = "Cochin";
            addTrainDTO.TotalSeats = 120;
            addTrainDTO.PricePerKM = 1;

            var mockAdminService = new Mock<IAdminService>();
            mockAdminService.Setup(s => s.AddTrainbyAdmin(addTrainDTO)).ReturnsAsync(new AddTrainReturnDTO());

            // Inject the mock
            adminController = new AdminController(mockAdminService.Object);

            var result = await adminController.AddTrain(addTrainDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AdminAddTrainFailTest()
        {
            //Arrange
            AddTrainDTO addTrainDTO = new AddTrainDTO();
            addTrainDTO.TrainNumber = 1;
            addTrainDTO.TrainName = "AmirthaExpress";
            addTrainDTO.TrainStartDate = DateTime.Now;
            addTrainDTO.TrainEndDate = DateTime.Now;
            addTrainDTO.ArrivalTime = DateTime.Now;
            addTrainDTO.DepartureTime = DateTime.Now;
            addTrainDTO.StartingPoint = "Madurai";
            addTrainDTO.EndingPoint = "Cochin";
            addTrainDTO.TotalSeats = 120;
            addTrainDTO.PricePerKM = 1;

            var Result1 = await adminController.AddTrain(addTrainDTO);
            //Arrange
            AddTrainDTO addTrainDTO2 = new AddTrainDTO();
            addTrainDTO2.TrainNumber = 1;
            addTrainDTO2.TrainName = "AmirthaExpress";
            addTrainDTO2.TrainStartDate = DateTime.Now;
            addTrainDTO2.TrainEndDate = DateTime.Now;
            addTrainDTO2.ArrivalTime = DateTime.Now;
            addTrainDTO2.DepartureTime = DateTime.Now;
            addTrainDTO2.StartingPoint = "Madurai";
            addTrainDTO2.EndingPoint = "Cochin";
            addTrainDTO2.TotalSeats = 120;
            addTrainDTO2.PricePerKM = 1;


            // Mocked AdminService behavior to throw TrainAlreadyAllotedException
            var mockAdminService = new Mock<IAdminService>();
            mockAdminService.Setup(s => s.AddTrainbyAdmin(addTrainDTO2)).ThrowsAsync(new TrainAlreadyAllotedException("Hey Admin! This Train is Already Alloted in your required date and status is Inline! Please, Try to add another availble Trains."));

            // Inject the mock
            adminController = new AdminController(mockAdminService.Object);

            ///Action
            var exception = Assert.ThrowsAsync<TrainAlreadyAllotedException>(async () => await adminController.AddTrain(addTrainDTO2));

            //Assert
            Assert.AreEqual("Hey Admin! This Train is Already Alloted in your required date and status is Inline! Please, Try to add another availble Trains.", exception.Message);
        }

        [Test]
        public async Task AdminAddTrainRouteSuccessTest()
        {
            //Arrange
            AddTrainRouteDTO addTrainRouteDTO = new AddTrainRouteDTO();
            addTrainRouteDTO.TrackId = 1;
            addTrainRouteDTO.TrackNumber = 1;
            addTrainRouteDTO.StationId = 2;
            addTrainRouteDTO.StopNumber = 1;
            addTrainRouteDTO.ArrivalTime = DateTime.Now;
            addTrainRouteDTO.DepartureTime = DateTime.Today;
            addTrainRouteDTO.RouteStartDate = DateTime.Now;
            addTrainRouteDTO.RouteEndDate = DateTime.Today;

            var mockAdminService = new Mock<IAdminService>();
            mockAdminService.Setup(s => s.AddTrainRoutebyAdmin(addTrainRouteDTO)).ReturnsAsync(new AddTrainRouteReturnDTO());

            // Inject the mock
            adminController = new AdminController(mockAdminService.Object);

            var result = await adminController.AddTrainRoute(addTrainRouteDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AdminAddTrouteFailTest()
        {
            //Arrange
            AddTrainRouteDTO addTrainRouteDTO = new AddTrainRouteDTO();
            addTrainRouteDTO.TrackId = 1;
            addTrainRouteDTO.TrackNumber = 1;
            addTrainRouteDTO.StationId = 2;
            addTrainRouteDTO.StopNumber = 1;
            addTrainRouteDTO.ArrivalTime = DateTime.Now;
            addTrainRouteDTO.DepartureTime = DateTime.Today;
            addTrainRouteDTO.RouteStartDate = DateTime.Now;
            addTrainRouteDTO.RouteEndDate = DateTime.Today;

            var Result1 = await adminController.AddTrainRoute(addTrainRouteDTO);
            //Arrange
            AddTrainRouteDTO addTrainRouteDTO2 = new AddTrainRouteDTO();
            addTrainRouteDTO2.TrackId = 1;
            addTrainRouteDTO2.TrackNumber = 1;
            addTrainRouteDTO2.StationId = 2;
            addTrainRouteDTO2.StopNumber = 1;
            addTrainRouteDTO2.ArrivalTime = DateTime.Now;
            addTrainRouteDTO2.DepartureTime = DateTime.Today;
            addTrainRouteDTO2.RouteStartDate = DateTime.Now;
            addTrainRouteDTO2.RouteEndDate = DateTime.Today;


            // Mocked AdminService behavior to throw TrainAlreadyAllotedException
            var mockAdminService = new Mock<IAdminService>();
            mockAdminService.Setup(s => s.AddTrainRoutebyAdmin(addTrainRouteDTO)).ThrowsAsync(new RequiredTrackBusyException("The Required Track is Busy! Please alter your Train Track Admin."));

            // Inject the mock
            adminController = new AdminController(mockAdminService.Object);

            ///Action
            var exception = Assert.ThrowsAsync<RequiredTrackBusyException>(async () => await adminController.AddTrainRoute(addTrainRouteDTO2));

            //Assert
            Assert.AreEqual("The Required Track is Busy! Please alter your Train Track Admin.", exception.Message);
        }
    }
}
