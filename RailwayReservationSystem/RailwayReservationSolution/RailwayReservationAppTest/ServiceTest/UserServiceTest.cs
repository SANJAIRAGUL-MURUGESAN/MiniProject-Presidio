using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.ReservationCancelExceptions;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;
using RailwayReservationApp.Repositories;
using RailwayReservationApp.Repositories.ReservationRequest;
using RailwayReservationApp.Repositories.TrainRequest;
using RailwayReservationApp.Services;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class Tests
    {
        RailwayReservationContext context;
        IUserService userService;

        [SetUp]
        public void Setup() { 
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

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
            userService = new UserServices(_ReservationRepository, _TrainRepository, _StationRepository, _TrainRequestforTrainRoutesRepository,
                _SeatRepository, _UserRepository, _UserDetailsRepository, _PaymentRepository, _RewardRepository, _ReservationCancelRepository,
                _TrainRequestforReservationsRepository, _ReservationRequestforSeatsRepository);
            Train addTrainDTO = new Train();
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
            var Routeresult = _TrainRepository.Add(addTrainDTO);


            BookTrainDTO bookTrainDTO = new BookTrainDTO();
            bookTrainDTO.StartingPoint = "Pollachi";
            bookTrainDTO.EndingPoint = "Cochin";
            bookTrainDTO.TrainDate = DateTime.Now;
            bookTrainDTO.TrainId = 1;
            bookTrainDTO.UserId = 1;
            bookTrainDTO.Seats = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
            bookTrainDTO.ClassName = "FirstClass";
            var TrainAdditionResult = userService.BookTrainByUser(bookTrainDTO);

            Seat seat = new Seat();
            seat.SeatNumber = 1;
            seat.ReservationId = 1;
            var SeatResult = _SeatRepository.Add(seat);

            CancelReservationDTO cancelReservationDTO = new CancelReservationDTO();
            cancelReservationDTO.SeatNumber = 1;
            cancelReservationDTO.RefundAmount = 200;
            cancelReservationDTO.ReservationCancelReason = "Emergency";
            cancelReservationDTO.ReservationId = 1;
            var CancelReservationResult = userService.CancelReservation(cancelReservationDTO);
        }

        [Test]
        public void UserRegisterSuccessTest()
        {
            //Arrange
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO();
            userRegisterDTO.Name = "Sanjai";
            userRegisterDTO.Email = "sanjai@gmail.com";
            userRegisterDTO.Address = "Tamilnadu";
            userRegisterDTO.PhoneNumber = "1234567890";
            userRegisterDTO.Gender = "Male";
            userRegisterDTO.Disability = false;
            userRegisterDTO.Password = "sanjai123";
            var result = userService.UserRegistration(userRegisterDTO);

            UserRegisterReturnDTO userRegisterReturnDTO = new UserRegisterReturnDTO();
            userRegisterReturnDTO.Name = "Sanjai";
            userRegisterReturnDTO.Email = "sanjai@gmail.com";
            userRegisterReturnDTO.Address = "Tamilnadu";
            userRegisterReturnDTO.PhoneNumber = "1234567890";
            userRegisterReturnDTO.Gender = "Male";
            userRegisterReturnDTO.Disability = false;

            var ans = userService.UserRegistration(userRegisterDTO);

            //Assert
            Assert.IsNotNull(ans);

        }

        [Test]
        public void UserLoginSuccessTest()
        {
            //Arrange
            UserLoginDTO userLoginDTO = new UserLoginDTO();
            userLoginDTO.UserId = 1;
            userLoginDTO.Password = "sanjai123";


            var ans = userService.Login(userLoginDTO);

            //Assert
            Assert.IsNotNull(ans);
        }

        [Test]
        public void UserLoginFailTest()
        {
            //Arrange
            UserLoginDTO userLoginDTO = new UserLoginDTO();
            userLoginDTO.UserId = 12345678;
            userLoginDTO.Password = "sanjai123";

            //Action
            var exception = Assert.ThrowsAsync<InvalidCredentialsException>(async() => await userService.Login(userLoginDTO));

            //Assert
            Assert.AreEqual("Invalid Username or Password", exception.Message);
        }

        [Test]
        public void BookTrainSuccessTest()
        {
            //Arrange

            BookTrainDTO bookTrainDTO = new BookTrainDTO();
            bookTrainDTO.StartingPoint = "Pollachi";
            bookTrainDTO.EndingPoint = "Cochin";
            bookTrainDTO.TrainDate = DateTime.Now;
            bookTrainDTO.TrainId = 1;
            bookTrainDTO.UserId = 1;
            bookTrainDTO.Seats = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
            bookTrainDTO.ClassName = "FirstClass";
            var TrainAdditionResult = userService.BookTrainByUser(bookTrainDTO);
            //Assert
            Assert.IsNotNull(TrainAdditionResult);
        }

        [Test]
        public void  BookTrainExceptionTest()
        {
            BookTrainDTO bookTrainDTO = new BookTrainDTO();
            bookTrainDTO.StartingPoint = "Pollachi";
            bookTrainDTO.EndingPoint = "Cochin";
            bookTrainDTO.TrainDate = DateTime.Now;
            bookTrainDTO.TrainId = 8;
            bookTrainDTO.UserId = 1;
            bookTrainDTO.ClassName = "FirstClass";
            bookTrainDTO.Seats = new List<int>(1);

            //Action
            var exception = Assert.ThrowsAsync<NoSuchTrainFoundException>(async() => await userService.BookTrainByUser(bookTrainDTO));

            //Assert
            Assert.AreEqual("No Such Train Found!", exception.Message);
        }

        [Test]
        public void BookTrainSeatExceptionTest()
        {
            BookTrainDTO bookTrainDTO = new BookTrainDTO();
            bookTrainDTO.StartingPoint = "Pollachi";
            bookTrainDTO.EndingPoint = "Cochin";
            bookTrainDTO.TrainDate = DateTime.Now;
            bookTrainDTO.TrainId = 1;
            bookTrainDTO.UserId = 1;
            bookTrainDTO.ClassName = "FirstClass";
            bookTrainDTO.Seats = new List<int>(1);

            //Action
            var exception = Assert.ThrowsAsync<SeatAlreadyReservedException>(async () => await userService.BookTrainByUser(bookTrainDTO));

            //Assert
            Assert.AreEqual("Seat Already Reserved User! Recheck and book available seats", exception.Message);
        }

        [Test]
        public void PaymentConfirmationTest()
        {
            AddPaymentDTO paymentDTO = new AddPaymentDTO();
            paymentDTO.PaymentDate = DateTime.Now;
            paymentDTO.PaymentMethod = "UPI";
            paymentDTO.Status = "Success";
            paymentDTO.Amount = 5000;
            paymentDTO.ReservationId = 17;

            var PaymentResult = userService.ConfirmPayment(paymentDTO);
            Assert.IsNotNull(PaymentResult);
        }

        [Test]
        public async Task PaymentConfirmationExceptionTest()
        {
            AddPaymentDTO paymentDTO = new AddPaymentDTO();
            paymentDTO.PaymentDate = DateTime.Now;
            paymentDTO.PaymentMethod = "UPI";
            paymentDTO.Status = "Success";
            paymentDTO.Amount = 5000;
            paymentDTO.ReservationId = 45678;

            //Action
            var exception = Assert.ThrowsAsync<NoSuchReservationFoundException>(async () => await userService.ConfirmPayment(paymentDTO));

            //Assert
            Assert.AreEqual("No Such Reservation Found!", exception.Message);
        }

        [Test]
        public void SerachTrainSuccessTest()
        {
            //Arrange

            SearchTrainDTO searchTrainDTO = new SearchTrainDTO();
            searchTrainDTO.TrainStartDate = DateTime.Now;
            searchTrainDTO.StartingPoint = "Madurai";
            searchTrainDTO.EndingPoint = "Cochin";
            var result = userService.SearchTrainByUser(searchTrainDTO);
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void SerachTrainFailTest()
        {
            //Arrange

            SearchTrainDTO searchTrainDTO = new SearchTrainDTO();
            searchTrainDTO.TrainStartDate = DateTime.Now;
            searchTrainDTO.StartingPoint = "Maduraifghj";
            searchTrainDTO.EndingPoint = "Cochinfghj";

            //Action
            var exception = Assert.ThrowsAsync<NoTrainsAvailableforyourSearchException>(async () => await userService.SearchTrainByUser(searchTrainDTO));

            //Assert
            Assert.AreEqual("Hey User, There is no Trains available for your search!", exception.Message);
        }

        [Test]
        public void CancelReservationSuccessTest()
        {
            //Arrange

            CancelReservationDTO cancelReservationDTO = new CancelReservationDTO();
            cancelReservationDTO.SeatNumber = 1;
            cancelReservationDTO.RefundAmount = 200;
            cancelReservationDTO.ReservationCancelReason = "Emergency";
            cancelReservationDTO.ReservationId = 1;
            var result = userService.CancelReservation(cancelReservationDTO);
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CancelReservationFailTest()
        {
            //Arrange

            CancelReservationDTO cancelReservationDTO = new CancelReservationDTO();
            cancelReservationDTO.SeatNumber = 1;
            cancelReservationDTO.RefundAmount = 200;
            cancelReservationDTO.ReservationCancelReason = "Emergency";
            cancelReservationDTO.ReservationId = 345671;

            //Action
            var exception = Assert.ThrowsAsync<NoSuchReservationFoundException>(async () => await userService.CancelReservation(cancelReservationDTO));

            //Assert
            Assert.AreEqual("No Such Reservation Found!", exception.Message);
        }

        [Test]
        public void GetAllClassesofaTrainSuccessTest()
        {
            //Arrange
            var result = userService.GetAllClassofTrain(1);
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllClassesofaTrainFailTest()
        {
            //Arrange
            //Action
            var exception = Assert.ThrowsAsync<NoSuchTrainFoundException>(async () => await userService.GetAllClassofTrain(145678));

            //Assert
            Assert.AreEqual("No Such Reservation Found!", exception.Message); ;
        }
    }
}

