using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class TrainRepositoryTest
    {
        RailwayReservationContext context;
        TrainRepository trainRepository;
        int TrainId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            trainRepository = new TrainRepository(context);
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

            var result = trainRepository.Add(trainRegister);
            TrainId = result.Id;
        }

        [Test]
        public async Task TrainDeleteSuccessTest()
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

            var DeletedResult = await trainRepository.Delete(result.TrainId);
            
            //Assert
            Assert.AreEqual("AmirthaExpress", DeletedResult.TrainName);
        }

        [Test]
        public async Task TrainDeleteFailTest()
        { 
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTrainFoundException>(async () => await trainRepository.Delete(13456));

            //Assert
            Assert.AreEqual("No Such Train Found!", exception.Message);
        }


    }
}
