using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class TrackRepositoryTest
    {
        RailwayReservationContext context;
        TrackRepository trackRepository;
        int trackId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            trackRepository = new TrackRepository(context);

            Track track = new Track();
            track.TrackStartingPoint = "Pollachi";
            track.TrackEndingPoint = "Madurai";
            track.TrackStatus = "Active";
            track.TrackNumber = 1;

            var Result = trackRepository.Add(track);

            trackId = Result.Id;
        }

        [Test]
        public async Task TrackDeleteSuccessTest()
        {
            //Arrange 
            Track track = new Track();
            track.TrackStartingPoint = "Pollachi";
            track.TrackEndingPoint = "Madurai";
            track.TrackStatus = "Active";
            track.TrackNumber = 1;

            var Result = await trackRepository.Add(track);
            var DeletedResult = await trackRepository.Delete(Result.TrackId);

            //Assert
            Assert.AreEqual(1, DeletedResult.TrackNumber);
        }

        [Test]
        public async Task TrackDeleteFailTest()
        {

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTrackFoundException>(async () => await trackRepository.Delete(13456));

            //Assert
            Assert.AreEqual("No Such Train Track Found!", exception.Message);
        }

        [Test]
        public async Task GetAllTrackSuccessTest()
        {
            ///Action
            var Result = await trackRepository.Get();

            //Assert
            Assert.NotNull(Result);
        }

        [Test]
        public async Task GetAllTrackFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTrainFoundException>(async () => await trackRepository.Get());

            //Assert
            Assert.AreEqual("No Tracks Found!", exception.Message);
        }

        [Test]
        public async Task UpdateTrainTrackSuccessTest()
        {
            //Arrange 
            Track track = new Track();
            track.TrackStartingPoint = "Pollachi";
            track.TrackEndingPoint = "Madurai";
            track.TrackStatus = "Active";
            track.TrackNumber = 1;

            var result = await trackRepository.Add(track);

            result.TrackNumber = 2;
            //Arrange
            var UpdatedResult = await trackRepository.Update(result);

            //Assert
            Assert.AreEqual(2, result.TrackNumber);
        }

        [Test]
        public async Task UpdateTrainTrackFailTest()
        {
            //Arrange 
            Track track = new Track();
            track.TrackStartingPoint = "Pollachi";
            track.TrackEndingPoint = "Madurai";
            track.TrackStatus = "Active";
            track.TrackNumber = 1;

            var result = await trackRepository.Add(track);

            result.TrackNumber = 2;

            //Action
            var exception = Assert.ThrowsAsync<NoSuchTrackFoundException>(async () => await trackRepository.Update(result));

            //Assert
            Assert.AreEqual("No Train Track Found!", exception.Message);
        }

    }
}
