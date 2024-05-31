using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.RewardExceptions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class RewardRepositoryTest
    {
        RailwayReservationContext context;
        RewardRepository rewardRepository;
        int RewardId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);
            rewardRepository = new RewardRepository(context);
        }

        [Test]
        public async Task AddRewardSuccessTest()
        {
            //Arrange
            Rewards reward = new Rewards();
            reward.RewardPoints = 10;
            reward.UserId = 1;

            var Result = await rewardRepository.Add(reward);

            //Assert
            Assert.AreEqual(1, Result.UserId);
        }

        [Test]
        public async Task RewardDeleteSuccessTest()
        {
            //Arrange
            Rewards reward = new Rewards();
            reward.RewardPoints = 10;
            reward.UserId = 1;

            var Result = await rewardRepository.Add(reward);
            var DeletedResult = await rewardRepository.Delete(Result.RewardId);

            //Assert
            Assert.AreEqual(1, DeletedResult.UserId);
        }

        [Test]
        public async Task RewardDeleteFailTest()
        {
            //Arrange
            Rewards reward = new Rewards();
            reward.RewardPoints = 10;
            reward.UserId = 1;

            var Result = await rewardRepository.Add(reward);

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRewardFoundException>(async () => await rewardRepository.Delete(13456));

            //Assert
            Assert.AreEqual("No Such Reward Found!", exception.Message);
        }

        [Test]
        public async Task GetRewardbyKeySuccessTest()
        {

            //Arrange
            Rewards reward = new Rewards();
            reward.RewardPoints = 10;
            reward.UserId = 1;

            var Result = await rewardRepository.Add(reward);

            var GetResult = await rewardRepository.GetbyKey(Result.RewardId);

            //Assert
            Assert.AreEqual(10, GetResult.RewardPoints);
        }

        [Test]
        public async Task GetRewardbyKeyFailTest()
        {
            //Arrange
            Rewards reward = new Rewards();
            reward.RewardPoints = 10;
            reward.UserId = 1;

            var Result = await rewardRepository.Add(reward);
            var GetResult = await rewardRepository.GetbyKey(13456);
            Assert.IsNull(GetResult);

            ///Action
            //var exception = Assert.ThrowsAsync<NoSuchRewardFoundException>(async () => await rewardRepository.GetbyKey(13456));

            ////Assert
            //Assert.AreEqual("No Such Reward Found!", exception.Message);
        }

        [Test]
        public async Task GetAllRewardSuccessTest()
        {
            //Arrange
            var result = await rewardRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllRewardFailTest()
        {
            //Action
            var exception = Assert.ThrowsAsync<NoRewardsFoundException>(async () => await rewardRepository.Get());

            //Assert
            Assert.AreEqual("No Rewards Found!", exception.Message);
        }

    }
}
