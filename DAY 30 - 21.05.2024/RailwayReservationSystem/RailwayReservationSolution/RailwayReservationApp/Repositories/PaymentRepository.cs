using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.PaymentExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class PaymentRepository : IRepository<int, Payment>
    {
        private readonly RailwayReservationContext _context;
        public PaymentRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Payment> Add(Payment item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Payment> Delete(int key)
        {
            var payment = await GetbyKey(key);
            if (payment != null)
            {
                _context.Remove(payment);
                _context.SaveChangesAsync(true);
                return payment;
            }
            throw new NoSuchPaymentFoundException();
        }
        public Task<Payment> GetbyKey(int key)
        {
            var payment = _context.Payments.FirstOrDefaultAsync(t => t.PaymentId == key);
            if (payment != null)
            {
                return payment;
            }
            throw new NoSuchPaymentFoundException();
        }
        public async Task<IEnumerable<Payment>> Get()
        {
            var payments = await _context.Payments.ToListAsync();
            if (payments != null)
            {
                return payments;
            }
            throw new NoPaymentsFoundException();
        }
        public async Task<Payment> Update(Payment item)
        {
            var payment = await GetbyKey(item.ReservationId);
            if (payment != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return payment;
            }
            throw new NoSuchPaymentFoundException();
        }
    }
}
