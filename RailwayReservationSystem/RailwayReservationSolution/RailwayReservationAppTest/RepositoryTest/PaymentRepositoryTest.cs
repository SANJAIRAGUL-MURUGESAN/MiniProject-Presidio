using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.PaymentExceptions;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationAppTest.RepositoryTest
{
    public class PaymentRepositoryTest
    {

        RailwayReservationContext context;
        PaymentRepository paymentRepository;
        int PaymentId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("ReservationDummyDB");
            context = new RailwayReservationContext(optionsBuilder.Options);

            paymentRepository = new PaymentRepository(context);
        }

        [Test]
        public async Task PaymentDeleteSuccessTest()
        {
            //Arrange
            Payment payment = new Payment();
            payment.PaymentMethod = "UPI";
            payment.Amount = 5000;
            payment.PaymentDate = DateTime.Now;
            payment.ReservationId = 1;
            payment.Status = "Success";

            var result = await paymentRepository.Add(payment);
            var DeletedResult = await paymentRepository.Delete(result.PaymentId);

            //Assert
            Assert.AreEqual(result.PaymentId, DeletedResult.PaymentId);
        }

        [Test]
        public async Task PaymentDeleteFailTest()
        {

            ///Action
            var exception = Assert.ThrowsAsync<NoSuchPaymentFoundException>(async () => await paymentRepository.Delete(13456));

            //Assert
            Assert.AreEqual("No Such Payment Found!", exception.Message);
        }
        [Test]
        public async Task GetAllPaymentSuccessTest()
        {
            //Arrange
            var result = await paymentRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetAllAdminFailTest()
        {
            //Action
            var exception = Assert.ThrowsAsync<NoPaymentsFoundException>(async () => await paymentRepository.Get());

            //Assert
            Assert.AreEqual("No Payments Found!", exception.Message);
        }


        [Test]
        public async Task UpdatePaymentSuccessTest()
        {
            //Arrange
            Payment payment = new Payment();
            payment.PaymentMethod = "UPI";
            payment.Amount = 5000;
            payment.PaymentDate = DateTime.Now;
            payment.ReservationId = 1;
            payment.Status = "Success";

            var result = await paymentRepository.Add(payment);

            result.Status = "Pending";
            //Arrange
            var UpdatedResult = await paymentRepository.Update(result);

            //Assert
            Assert.AreEqual("Pending", UpdatedResult.Status);
        }

        [Test]
        public async Task UpdatePaymentFailTest()
        {
            //Arrange
            Payment payment = new Payment();
            payment.PaymentMethod = "UPI";
            payment.Amount = 5000;
            payment.PaymentDate = DateTime.Now;
            payment.ReservationId = 1;
            payment.Status = "Success";

            var result = await paymentRepository.Add(payment);

            result.PaymentId = 123;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchPaymentFoundException>(async () => await paymentRepository.Update(result));

            //Assert
            Assert.AreEqual("No Such Payment Found!", exception.Message);
        }
    }
}
