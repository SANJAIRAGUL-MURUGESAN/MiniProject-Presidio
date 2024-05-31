using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.ReservationCancelExceptions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class ReservationCancelRepositoryTest
    {
        RailwayReservationContext context;
        ReservationCancelRepository reservationCancelRepository;
        int CancelId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            reservationCancelRepository = new ReservationCancelRepository(context);
        }

        [Test]
        public async Task ReservatoinCancelDeleteSuccessTest()
        {
            //Arrange
            ReservationCancel reservationCancel = new ReservationCancel();
            reservationCancel.RefundAmount = 5000;
            reservationCancel.SeatNumber = 2;
            reservationCancel.ReservationId = 1;
            reservationCancel.ReservationCancelReason = "Emergency";
            var Result = await reservationCancelRepository.Add(reservationCancel);

            var DeletedResult = await reservationCancelRepository.Delete(Result.ReservationCancelId);

            //Assert
            Assert.AreEqual(2, DeletedResult.SeatNumber);
        }

        [Test]
        public async Task ReservationCancelDeleteFailTest()
        {

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchReservationCancelFoundException>(async () => await reservationCancelRepository.Delete(13456));

            //Assert
            Assert.AreEqual("No Such Reservation Cancel Found!", exception.Message);
        }

        [Test]
        public async Task GetAllReservationCancelSuccessTest()
        {
            //Arrange
            var result = await reservationCancelRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllReservationCancelFailTest()
        {
            //Action
            var exception = Assert.ThrowsAsync<NoAdminsFoundException>(async () => await reservationCancelRepository.Get());

            //Assert
            Assert.AreEqual("No Reservations Found!", exception.Message);
        }

        [Test]
        public async Task UpdateAdminSuccessTest()
        {
            //Arrange
            ReservationCancel reservationCancel = new ReservationCancel();
            reservationCancel.RefundAmount = 5000;
            reservationCancel.SeatNumber = 2;
            reservationCancel.ReservationId = 1;
            reservationCancel.ReservationCancelReason = "Emergency";
            var Result = await reservationCancelRepository.Add(reservationCancel);

            Result.RefundAmount = 5678;
            //Arrange
            var UpdatedResult = await reservationCancelRepository.Update(Result);

            //Assert
            Assert.AreEqual(5678, UpdatedResult.RefundAmount);
        }
    }
}
