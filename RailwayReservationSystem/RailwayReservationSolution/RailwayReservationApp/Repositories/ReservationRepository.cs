using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class ReservationRepository : IRepository<int, Reservation>
    {
        protected readonly RailwayReservationContext _context;
        public ReservationRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Reservation> Add(Reservation item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Reservation> Delete(int key)
        {
            var reservation = await GetbyKey(key);
            if (reservation != null)
            {
                _context.Remove(reservation);
                _context.SaveChangesAsync(true);
                return reservation;
            }
            throw new NoSuchReservationFoundException();
        }
        public virtual Task<Reservation> GetbyKey(int key)
        {
            var reservation = _context.Reservations.FirstOrDefaultAsync(t => t.ReservationId == key);
            if (reservation != null)
            {
                return reservation;
            }
            throw new NoSuchReservationFoundException();
        }
        public async Task<IEnumerable<Reservation>> Get()
        {
            var reservations = await _context.Reservations.ToListAsync();
            if (reservations != null)
            {
                return reservations;
            }
            throw new NoReservationsFoundException();
        }

        public async Task<Reservation> Update(Reservation item)
        {
            var reservation = await GetbyKey(item.ReservationId);
            if (reservation != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return reservation;
            }
            throw new NoSuchReservationFoundException();
        }
    }
}
