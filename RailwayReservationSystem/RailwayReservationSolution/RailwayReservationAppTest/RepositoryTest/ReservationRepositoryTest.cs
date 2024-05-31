using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.ReservationCancelExceptions;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class ReservationRepositoryTest
    {
        RailwayReservationContext context;
        ReservationRepository reservationRepository;
        int AdminId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);
            reservationRepository = new ReservationRepository(context);
        }

        [Test]
        public async Task AddReservationSuccessTest()
        {
            //Arrange
            Reservation reservation = new Reservation();
            reservation.Amount = 5000;
            reservation.TrainClassName = "FirstClass";
            reservation.UserId = 1;
            reservation.ReservationDate = DateTime.Now;
            reservation.TrainId = 1;
            reservation.TrainDate = DateTime.Now;
            reservation.StartingPoint = "Madurai";
            reservation.EndingPoint = "Cochin";
            reservation.Status = "PaymentPending";

            var result = await reservationRepository.Add(reservation);

            //Assert
            Assert.AreEqual(5000, result.Amount);
        }

        [Test]
        public async Task DeleteReservationSuccessTest()
        {
            //Arrange
            Reservation reservation = new Reservation();
            reservation.Amount = 5000;
            reservation.TrainClassName = "FirstClass";
            reservation.UserId = 1;
            reservation.ReservationDate = DateTime.Now;
            reservation.TrainId = 1;
            reservation.TrainDate = DateTime.Now;
            reservation.StartingPoint = "Madurai";
            reservation.EndingPoint = "Cochin";
            reservation.Status = "PaymentPending";

            var result = await reservationRepository.Add(reservation);

            var DeletedResult = await reservationRepository.Delete(result.ReservationId);

            //Assert
            Assert.AreEqual(result.ReservationId, DeletedResult.ReservationId);
        }

        [Test]
        public async Task DeleteReservationFailTest()
        {
            //Arrange
            Reservation reservation = new Reservation();
            reservation.Amount = 5000;
            reservation.TrainClassName = "FirstClass";
            reservation.UserId = 1;
            reservation.ReservationDate = DateTime.Now;
            reservation.TrainId = 1;
            reservation.TrainDate = DateTime.Now;
            reservation.StartingPoint = "Madurai";
            reservation.EndingPoint = "Cochin";
            reservation.Status = "PaymentPending";

            var result = await reservationRepository.Add(reservation);

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchReservationFoundException>(async () => await reservationRepository.Delete(13456));

            //Assert
            Assert.AreEqual("No Such Reservation Found!", exception.Message);

        }

    }
}
