using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.TrainRoutesExceptions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class TrainRouteRepositoryTest
    {
        RailwayReservationContext context;
        TrainRouteRepository trainRouteRepository;
        TrainRepository trainRepository;
        StationRepository stationRepository;
        TrackRepository trackRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);
            trainRouteRepository = new TrainRouteRepository(context);
            trainRepository = new TrainRepository(context);
            stationRepository = new StationRepository(context);
            trackRepository = new TrackRepository(context);
        }

        [Test]
        public async Task TrainRouteSuccessTest()
        {
            //Arrange
            Train trainRegister = new Train();
            trainRegister.StartingPoint = "Madurai";
            trainRegister.EndingPoint = "Cochin";
            trainRegister.TrainStartDate = DateTime.Now;
            trainRegister.TrainEndDate = DateTime.Now;
            trainRegister.DepartureTime = DateTime.Now;
            trainRegister.ArrivalTime = DateTime.Now;
            trainRegister.TotalSeats = 120;
            trainRegister.TrainName = "AmirthaExpress";
            trainRegister.TrainNumber = 1;
            trainRegister.PricePerKM = 1;
            trainRegister.TrainStatus = "Inline";
            var Trainresult = await trainRepository.Add(trainRegister);

            Station station = new Station();
            station.StationName = "Madurai";
            station.StationState = "Tamilnadu";
            station.StationPincode = "2345678";

            var Stationresult = stationRepository.Add(station);

            Track track = new Track();
            track.TrackStartingPoint = "Pollachi";
            track.TrackEndingPoint = "Madurai";
            track.TrackStatus = "Active";
            track.TrackNumber = 1;

            var Trackresult = trackRepository.Add(track);

            TrainRoutes trainRoutes = new TrainRoutes();
            trainRoutes.RouteStartDate = DateTime.Now;
            trainRoutes.RouteEndDate = DateTime.Now;
            trainRoutes.ArrivalTime = DateTime.Now;
            trainRoutes.DepartureTime = DateTime.Now;
            trainRoutes.StopNumber = 2;
            trainRoutes.TrackNumber = 1;
            trainRoutes.StationId = Stationresult.Id;
            trainRoutes.TrackId = Trackresult.Id;
            trainRoutes.KilometerDistance = 100;
            trainRoutes.TrainId = Trainresult.TrainId;

            var Result = await trainRouteRepository.Add(trainRoutes);

            var DeletedResult = await trainRouteRepository.Delete(Result.RouteId);

            //Assert
            Assert.AreEqual(1, DeletedResult.TrainId);
        }

        [Test]
        public async Task TrainRouteFailTest()
        {
            //Arrange
            Train trainRegister = new Train();
            trainRegister.StartingPoint = "Madurai";
            trainRegister.EndingPoint = "Cochin";
            trainRegister.TrainStartDate = DateTime.Now;
            trainRegister.TrainEndDate = DateTime.Now;
            trainRegister.DepartureTime = DateTime.Now;
            trainRegister.ArrivalTime = DateTime.Now;
            trainRegister.TotalSeats = 120;
            trainRegister.TrainName = "AmirthaExpress";
            trainRegister.TrainNumber = 1;
            trainRegister.PricePerKM = 1;
            trainRegister.TrainStatus = "Inline";
            var Trainresult = await trainRepository.Add(trainRegister);

            Station station = new Station();
            station.StationName = "Madurai";
            station.StationState = "Tamilnadu";
            station.StationPincode = "2345678";

            var Stationresult = stationRepository.Add(station);

            Track track = new Track();
            track.TrackStartingPoint = "Pollachi";
            track.TrackEndingPoint = "Madurai";
            track.TrackStatus = "Active";
            track.TrackNumber = 1;

            var Trackresult = trackRepository.Add(track);

            TrainRoutes trainRoutes = new TrainRoutes();
            trainRoutes.RouteStartDate = DateTime.Now;
            trainRoutes.RouteEndDate = DateTime.Now;
            trainRoutes.ArrivalTime = DateTime.Now;
            trainRoutes.DepartureTime = DateTime.Now;
            trainRoutes.StopNumber = 2;
            trainRoutes.TrackNumber = 1;
            trainRoutes.StationId = Stationresult.Id;
            trainRoutes.TrackId = Trackresult.Id;
            trainRoutes.KilometerDistance = 100;
            trainRoutes.TrainId = Trainresult.TrainId;

            var Result = await trainRouteRepository.Add(trainRoutes);

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTrainRouteFoundException>(async () => await trainRouteRepository.Delete(Result.RouteId=234));

            //Assert
            Assert.AreEqual("No Such Train Route Found!", exception.Message);
        }

        [Test]
        public async Task GetTrainbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTrainRouteFoundException>(async () => await trainRouteRepository.GetbyKey(13456));

            //Assert
            Assert.AreEqual("No Such Train Route Found!", exception.Message);
        }

        [Test]
        public async Task GetAllTrainRoutesSuccessTest()
        {
            //Arrange
            var result = await trainRouteRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTrainRoutesFailTest()
        {
            //Arrange
            var result = await trainRouteRepository.Get();

            ///Action
            var exception = Assert.ThrowsAsync<NoTrainRoutesFoundException>(async () => await trainRouteRepository.Get());

            //Assert
            Assert.AreEqual("No Train Routes Found!", exception.Message);
        }


        [Test]
        public async Task UpdateTrainRouteSuccessTest()
        {
            Train trainRegister = new Train();
            trainRegister.StartingPoint = "Madurai";
            trainRegister.EndingPoint = "Cochin";
            trainRegister.TrainStartDate = DateTime.Now;
            trainRegister.TrainEndDate = DateTime.Now;
            trainRegister.DepartureTime = DateTime.Now;
            trainRegister.ArrivalTime = DateTime.Now;
            trainRegister.TotalSeats = 120;
            trainRegister.TrainName = "AmirthaExpress";
            trainRegister.TrainNumber = 1;
            trainRegister.PricePerKM = 1;
            trainRegister.TrainStatus = "Inline";
            var Trainresult = await trainRepository.Add(trainRegister);

            Station station = new Station();
            station.StationName = "Madurai";
            station.StationState = "Tamilnadu";
            station.StationPincode = "2345678";

            var Stationresult = stationRepository.Add(station);

            Track track = new Track();
            track.TrackStartingPoint = "Pollachi";
            track.TrackEndingPoint = "Madurai";
            track.TrackStatus = "Active";
            track.TrackNumber = 1;

            var Trackresult = trackRepository.Add(track);
            TrainRoutes trainRoutes = new TrainRoutes();
            trainRoutes.RouteStartDate = DateTime.Now;
            trainRoutes.RouteEndDate = DateTime.Now;
            trainRoutes.ArrivalTime = DateTime.Now;
            trainRoutes.DepartureTime = DateTime.Now;
            trainRoutes.StopNumber = 2;
            trainRoutes.TrackNumber = 1;
            trainRoutes.StationId = Stationresult.Id;
            trainRoutes.TrackId = Trackresult.Id;
            trainRoutes.KilometerDistance = 100;
            trainRoutes.TrainId = Trainresult.TrainId;

            var Result = await trainRouteRepository.Add(trainRoutes);

            Result.TrackNumber = 2;

            var UpdatedResult = await trainRouteRepository.Update(Result);

            //Assert
            Assert.AreEqual(2, UpdatedResult.TrackNumber);
        }

    }
}
