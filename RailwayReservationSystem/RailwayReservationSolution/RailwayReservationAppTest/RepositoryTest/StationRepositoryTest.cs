using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.StationExceptions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class StationRepositoryTest
    {
        RailwayReservationContext context;
        StationRepository stationRepository;
        int stationId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            stationRepository = new StationRepository(context);

            Station station = new Station();
            station.StationName = "Madurai";
            station.StationState = "Tamilnadu";
            station.StationPincode = "2345678";

            var Result = stationRepository.Add(station);
            stationId = Result.Id;
        }

        [Test]
        public async Task StationDeleteSuccessTest()
        {
            //Arrange
            Station station = new Station();
            station.StationName = "Madurai";
            station.StationState = "Tamilnadu";
            station.StationPincode = "2345678";

            var Result = await stationRepository.Add(station);
            var DeletedResult = await stationRepository.Delete(stationId);

            //Assert
            Assert.AreEqual("Madurai", DeletedResult.StationName);
        }

        [Test]
        public async Task StationDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchStationFoundException>(async () => await stationRepository.Delete(567));

            //Assert
            Assert.AreEqual("No Such Station Found!", exception.Message);
        }

        [Test]
        public async Task UpdateStationSuccessTest()
        {
            // Arrange
            Station station = new Station();
            station.StationName = "Madurai";
            station.StationState = "Tamilnadu";
            station.StationPincode = "2345678";
            var result = await stationRepository.Add(station);

            result.StationName = "MADURAI";
            //Arrange
            var UpdatedResult = await stationRepository.Update(result);

            //Assert
            Assert.AreEqual("MADURAI", result.StationName);
        }
        [Test]
        public async Task UpdateStationFailTest()
        {
            // Arrange
            Station station = new Station();
            station.StationName = "Madurai";
            station.StationState = "Tamilnadu";
            station.StationPincode = "2345678";
            var result = await stationRepository.Add(station);

            result.StationName = "MADURAI";

            result.StationId = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchStationFoundException>(async () => await stationRepository.Update(result));

            //Assert
            Assert.AreEqual("No Such Station Found!", exception.Message);
        }
    }
}
