using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class TrainClassRepositoryTest
    {
        RailwayReservationContext context;
        TrainClassRepository trainClassRepository;
        TrainRepository trainRepository;
        int TrainCLassId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            trainClassRepository = new TrainClassRepository(context);
            trainRepository = new TrainRepository(context);
        }

        [Test]
        public async Task UserDeleteSuccessTest()
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
            var result = await trainRepository.Add(trainRegister);

            TrainClass trainClass = new TrainClass();
            trainClass.TrainId = result.TrainId;
            trainClass.ClassName = "FirstClass";
            trainClass.ClassPrice = 10;
            trainClass.StartingSeatNumber = 1;
            trainClass.EndingSeatNumber = 30;
            var ClassResult = await trainClassRepository.Add(trainClass);

            var DeletedResult = await trainClassRepository.Delete(ClassResult.ClassId);

            //Assert
            Assert.AreEqual("FirstClass", DeletedResult.ClassName);
        }

        [Test]
        public async Task GetAllClassesSuccessTest()
        {
            //Arrange
            var result = await trainClassRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllClassesFailTest()
        {
            //Action
            var exception = Assert.ThrowsAsync<NoTrainTracksFoundException>(async () => await trainClassRepository.Get());

            //Assert
            Assert.AreEqual("No Train Tracks Founs!", exception.Message);
        }

        [Test]
        public async Task UpdateClasssSuccessTest()
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
            var result = await trainRepository.Add(trainRegister);

            TrainClass trainClass = new TrainClass();
            trainClass.TrainId = result.TrainId;
            trainClass.ClassName = "FirstClass";
            trainClass.ClassPrice = 10;
            trainClass.StartingSeatNumber = 1;
            trainClass.EndingSeatNumber = 30;
            var ClassResult = await trainClassRepository.Add(trainClass);

            ClassResult.ClassName = "FIRSTCLASS";
            //Arrange
            var UpdatedResult = await trainClassRepository.Update(ClassResult);

            //Assert
            Assert.AreEqual("FIRSTCLASS", UpdatedResult.ClassName);
        }

    }
}
