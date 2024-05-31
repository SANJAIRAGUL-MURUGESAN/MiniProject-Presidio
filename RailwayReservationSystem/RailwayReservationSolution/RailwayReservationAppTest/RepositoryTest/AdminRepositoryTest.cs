using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories.ReservationRequest;
using RailwayReservationApp.Repositories.StationRequest;
using RailwayReservationApp.Repositories.TrackRequest;
using RailwayReservationApp.Repositories.TrainRequest;
using RailwayReservationApp.Repositories;
using RailwayReservationApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailwayReservationApp.Exceptions.AdminExceptions;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class AdminRepositoryTest
    {

        RailwayReservationContext context;
        AdminRepository adminRepository;
        int AdminId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            adminRepository = new AdminRepository(context);
            Admin adminRegister = new Admin();
            adminRegister.Name = "sanjai";
            adminRegister.Address = "Tamilnadu";
            adminRegister.Gender = "Male";
            adminRegister.PhoneNumber = "234567890";
            adminRegister.Disability = false;
            adminRegister.Email = "sanjai@gmail.com";
            adminRegister.Password = "sanjai123";

            var result = adminRepository.Add(adminRegister);
            AdminId = result.Id;
        }


        [Test]
        public async Task AdminRegisterSuccessTest()
        {
            //Arrange
            Admin adminRegister = new Admin();
            adminRegister.Name = "sanjai";
            adminRegister.Address = "Tamilnadu";
            adminRegister.Gender = "Male";
            adminRegister.PhoneNumber = "234567890";
            adminRegister.Disability = false;
            adminRegister.Email = "sanjai@gmail.com";
            adminRegister.Password = "sanjai123";

            var result = await adminRepository.Add(adminRegister);

            //Assert
            Assert.AreEqual("sanjai",result.Name);
        }

        [Test]
        public async Task AdminDeleteSuccessTest()
        {
            //Arrange
            Admin adminRegister = new Admin();
            adminRegister.Name = "sanjai";
            adminRegister.Address = "Tamilnadu";
            adminRegister.Gender = "Male";
            adminRegister.PhoneNumber = "234567890";
            adminRegister.Disability = false;
            adminRegister.Email = "sanjai@gmail.com";
            adminRegister.Password = "sanjai123";

            var result = await adminRepository.Add(adminRegister);
            var DeletedResult = await adminRepository.Delete(result.Id);

            //Assert
            Assert.IsNotNull("sanjai", DeletedResult.Name);
        }

        [Test]
        public async Task AdminDeleteFailTest()
        {

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchAdminFoundException>(async () => await adminRepository.Delete(13456));

            //Assert
            Assert.AreEqual("No Such Admin Found!", exception.Message);
        }

        [Test]
        public async Task GetAdminbyKeySuccessTest()
        {
            //Arrange
            var result = await adminRepository.GetbyKey(AdminId);

            //Assert
            Assert.AreEqual("sanjai", result.Name);
        }

        [Test]
        public async Task GetAdminbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchAdminFoundException>(async () => await adminRepository.GetbyKey(13456));

            //Assert
            Assert.AreEqual("No Such Admin Found!", exception.Message);
        }

        [Test]
        public async Task GetAllAdminSuccessTest()
        {
            //Arrange
            var result = await adminRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllAdminFailTest()
        {
            //Action
            var exception = Assert.ThrowsAsync<NoAdminsFoundException>(async () => await adminRepository.Get());

            //Assert
            Assert.AreEqual("No Admins Found!", exception.Message);
        }

        [Test]
        public async Task UpdateAdminSuccessTest()
        {
            Admin adminRegister = new Admin();
            adminRegister.Name = "sanjai";
            adminRegister.Address = "Tamilnadu";
            adminRegister.Gender = "Male";
            adminRegister.PhoneNumber = "234567890";
            adminRegister.Disability = false;
            adminRegister.Email = "sanjai@gmail.com";
            adminRegister.Password = "sanjai123";

            var result = await adminRepository.Add(adminRegister);

            result.Name = "SanjaiRagul";
            //Arrange
            var UpdatedResult = await adminRepository.Update(result);

            //Assert
            Assert.AreEqual("SanjaiRagul", result.Name);
        }

        [Test]
        public async Task UpdateAdminFailTest()
        {
            Admin adminRegister = new Admin();
            adminRegister.Name = "sanjai";
            adminRegister.Address = "Tamilnadu";
            adminRegister.Gender = "Male";
            adminRegister.PhoneNumber = "234567890";
            adminRegister.Disability = false;
            adminRegister.Email = "sanjai@gmail.com";
            adminRegister.Password = "sanjai123";

            var result = await adminRepository.Add(adminRegister);

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchAdminFoundException>(async () => await adminRepository.Update(result));

            //Assert
            Assert.AreEqual("No Such Admin Found!", exception.Message);
        }

    }
}
