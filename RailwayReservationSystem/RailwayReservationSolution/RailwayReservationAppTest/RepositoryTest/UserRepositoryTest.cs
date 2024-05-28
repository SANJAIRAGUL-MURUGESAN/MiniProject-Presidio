using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
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
                                                        .UseInMemoryDatabase("dummyDB");
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
            //Action
            userService = new UserServices(_ReservationRepository, _TrainRepository, _StationRepository, _TrainRequestforTrainRoutesRepository,
                _SeatRepository, _UserRepository, _UserDetailsRepository, _PaymentRepository, _RewardRepository, _ReservationCancelRepository,
                _TrainRequestforReservationsRepository, _ReservationRequestforSeatsRepository);

            // Clear existing data
            context.Trains.RemoveRange(context.Trains);
            context.SaveChanges();


        }

        [Test]
        public void UserRegisterSuccessTest()
        {
            //Arrange
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
            //Action

            IUserService userService = new UserServices(_ReservationRepository, _TrainRepository, _StationRepository, _TrainRequestforTrainRoutesRepository,
                _SeatRepository, _UserRepository, _UserDetailsRepository, _PaymentRepository, _RewardRepository, _ReservationCancelRepository,
                _TrainRequestforReservationsRepository, _ReservationRequestforSeatsRepository);

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
        public void BookTrainSuccessTest()
        {
            //Arrange
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
            //Action
            IUserService userService = new UserServices(_ReservationRepository, _TrainRepository, _StationRepository, _TrainRequestforTrainRoutesRepository,
                _SeatRepository, _UserRepository, _UserDetailsRepository, _PaymentRepository, _RewardRepository, _ReservationCancelRepository,
                _TrainRequestforReservationsRepository, _ReservationRequestforSeatsRepository);

            BookTrainDTO bookTrainDTO = new BookTrainDTO();
            bookTrainDTO.StartingPoint = "Pollachi";
            bookTrainDTO.EndingPoint = "Cochin";
            bookTrainDTO.TrainDate = DateTime.Now;
            bookTrainDTO.TrainId = 1;
            bookTrainDTO.UserId = 1;
            bookTrainDTO.Seats = new List<int>(1);
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
            bookTrainDTO.Seats = new List<int>(1);

            //Action
            var exception = Assert.Throws<NoSuchTrainFoundException>(() => userService.BookTrainByUser(bookTrainDTO));

            //Assert
            Assert.AreEqual("No Such Train Found!", exception.Message);
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
        public void PaymentConfirmationExceptionTest()
        {
            AddPaymentDTO paymentDTO = new AddPaymentDTO();
            paymentDTO.PaymentDate = DateTime.Now;
            paymentDTO.PaymentMethod = "UPI";
            paymentDTO.Status = "Success";
            paymentDTO.Amount = 5000;
            paymentDTO.ReservationId = 45678;

            //Action
            var exception = Assert.Throws<NoSuchReservationFoundException>(() => userService.ConfirmPayment(paymentDTO));

            //Assert
            Assert.AreEqual("No Such Reservation Found!", exception.Message);
        }
    }
}

