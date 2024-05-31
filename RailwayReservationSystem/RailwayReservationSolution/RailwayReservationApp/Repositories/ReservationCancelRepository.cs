using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.ReservationCancelExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class ReservationCancelRepository : IRepository<int, ReservationCancel>
    {
        private readonly RailwayReservationContext _context;
        public ReservationCancelRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<ReservationCancel> Add(ReservationCancel item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<ReservationCancel> Delete(int key)
        {
            var reservationCancel = await GetbyKey(key);
            if (reservationCancel != null)
            {
                _context.Remove(reservationCancel);
                _context.SaveChangesAsync(true);
                return reservationCancel;
            }
            throw new NoSuchReservationCancelFoundException();
        }
        public async Task<ReservationCancel> GetbyKey(int key)
        {
            var reservationCancel = await _context.ReservationCancels.FirstOrDefaultAsync(t => t.ReservationCancelId == key);
            if (reservationCancel != null)
            {
                return reservationCancel;
            }
            throw new NoSuchReservationCancelFoundException();
        }
        public async Task<IEnumerable<ReservationCancel>> Get()
        {
            var reservationCancels = await _context.ReservationCancels.ToListAsync();
            if (reservationCancels != null)
            {
                return reservationCancels;
            }
            throw new NoReservationCancelsFoundException();
        }
        public async Task<ReservationCancel> Update(ReservationCancel item)
        {
            var user = await GetbyKey(item.ReservationCancelId);
            if (user != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return user;
            }
            throw new NoSuchReservationCancelFoundException();
        }
    }
}
