using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
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
    public class UserRepositoryTest2
    {

        RailwayReservationContext context;
        UserRepository userRepository;
        int UserId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            userRepository = new UserRepository(context);
            Users userRegister = new Users();
            userRegister.Name = "sanjai";
            userRegister.Address = "Tamilnadu";
            userRegister.Gender = "Male";
            userRegister.PhoneNumber = "234567890";
            userRegister.Disability = false;
            userRegister.Email = "sanjai@gmail.com";

            var result = userRepository.Add(userRegister);
            UserId = result.Id;
        }

        [Test]
        public async Task UserDeleteSuccessTest()
        {
            //Arrange
            Users userRegister = new Users();
            userRegister.Name = "sanjai";
            userRegister.Address = "Tamilnadu";
            userRegister.Gender = "Male";
            userRegister.PhoneNumber = "234567890";
            userRegister.Disability = false;
            userRegister.Email = "sanjai@gmail.com";

            var result = await userRepository.Add(userRegister);
            var DeletedResult = await userRepository.Delete(result.Id);

            //Assert
            Assert.AreEqual("sanjai", DeletedResult.Name);
        }

        [Test]
        public async Task UserDeleteFailTest()
        {

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchUserFoundException>(async () => await userRepository.Delete(13456));

            //Assert
            Assert.AreEqual("No Such User Found!", exception.Message);
        }

        [Test]
        public async Task GetUserbyKeySuccessTest()
        {
            //Arrange
            var result = await userRepository.GetbyKey(UserId);

            //Assert
            Assert.AreEqual("sanjai", result.Name);
        }

        [Test]
        public async Task GetUserbyKeyFailTest()
        {
            userRepository = new UserRepository(context);
            Users userRegister = new Users();
            userRegister.Name = "sanjai";
            userRegister.Address = "Tamilnadu";
            userRegister.Gender = "Male";
            userRegister.PhoneNumber = "234567890";
            userRegister.Disability = false;
            userRegister.Email = "sanjai@gmail.com";

            var result = userRepository.Add(userRegister);
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchUserFoundException>(async () => await userRepository.GetbyKey(result.Id+1));

            //Assert
            Assert.AreEqual("No Such User Found!", exception.Message);
        }

        [Test]
        public async Task GetAllUserSuccessTest()
        {
            //Arrange
            var result = await userRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllUserFailTest()
        {
            //Action
            var exception = Assert.ThrowsAsync<NoAdminsFoundException>(async () => await userRepository.Get());

            //Assert
            Assert.IsNull(exception);
        }

        [Test]
        public async Task UpdateUserSuccessTest()
        { 
            Users userRegister = new Users();
            userRegister.Name = "sanjai";
            userRegister.Address = "Tamilnadu";
            userRegister.Gender = "Male";
            userRegister.PhoneNumber = "234567890";
            userRegister.Disability = false;
            userRegister.Email = "sanjai@gmail.com";

            var result = await userRepository.Add(userRegister);

            result.Name = "SanjaiRagul";
            //Arrange
            var UpdatedResult = await userRepository.Update(result);

            //Assert
            Assert.AreEqual("SanjaiRagul", result.Name);
        }

        [Test]
        public async Task UpdateUserFailTest()
        {
            Users userRegister = new Users();
            userRegister.Name = "sanjai";
            userRegister.Address = "Tamilnadu";
            userRegister.Gender = "Male";
            userRegister.PhoneNumber = "234567890";
            userRegister.Disability = false;
            userRegister.Email = "sanjai@gmail.com";

            var result = await userRepository.Add(userRegister);

            result.Name = "SanjaiRagul";

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoAdminsFoundException>(async () => await userRepository.Update(result));

            //Assert
            Assert.AreEqual("No Admins Found!", exception.Message);
        }

    }
}
