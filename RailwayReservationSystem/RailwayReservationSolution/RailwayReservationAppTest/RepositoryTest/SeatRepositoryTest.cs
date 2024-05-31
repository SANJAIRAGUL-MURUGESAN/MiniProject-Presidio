using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class SeatRepositoryTest
    {
        RailwayReservationContext context;
        SeatRepository seatRepository;
        int seatId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            seatRepository = new SeatRepository(context);
        }

        [Test]
        public async Task GetAllSeatsSuccessTest()
        {
            //Arrange
            var result = await seatRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllSeatsFailTest()
        {
            //Action
            var exception = Assert.ThrowsAsync<NoSeatsFoundException>(async () => await seatRepository.Get());

            //Assert
            Assert.AreEqual(null, exception);
        }

        [Test]
        public async Task UpdateSeatSuccessTest()
        {
            Seat seat = new Seat();
            seat.ReservationId = 1;
            seat.SeatNumber = 1;

            var result = await seatRepository.Add(seat);

            result.SeatNumber = 23;
            //Arrange
            var UpdatedResult = await seatRepository.Update(result);

            //Assert
            Assert.AreEqual(23, UpdatedResult.SeatNumber);
        }

        [Test]
        public async Task UpdateSeatFailTest()
        {
            Seat seat = new Seat();
            seat.ReservationId = 1;
            seat.SeatNumber = 1;

            var result = await seatRepository.Add(seat);

            result.SeatId = 23;

            var exception = Assert.ThrowsAsync<NoSuchAdminFoundException>(async () => await seatRepository.Update(result));

            //Assert
            Assert.AreEqual("No Such Seat Found!", exception.Message);
        }
    }
}
