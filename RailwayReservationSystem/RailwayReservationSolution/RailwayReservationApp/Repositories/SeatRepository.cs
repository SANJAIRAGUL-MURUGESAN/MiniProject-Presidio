using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class SeatRepository : IRepository<int, Seat>
    {
        private readonly RailwayReservationContext _context;
        public SeatRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Seat> Add(Seat item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Seat> Delete(int key)
        {
            var seat = await GetbyKey(key);
            if (seat != null)
            {
                _context.Remove(seat);
                await _context.SaveChangesAsync(true);
                return seat;
            }
            throw new NoSuchSeatFoundException();
        }
        public async Task<Seat> GetbyKey(int key)
        {
            var seat = await _context.Seats.FirstOrDefaultAsync(t => t.SeatId == key);
            if (seat != null)
            {
                return seat;
            }
            throw new NoSuchSeatFoundException();
        }
        public async Task<IEnumerable<Seat>> Get()
        {
            var seats = await _context.Seats.ToListAsync();
            if (seats != null)
            {
                return seats;
            }
            throw new NoSeatsFoundException();
        }
        public async Task<Seat> Update(Seat item)
        {
            var seat = await GetbyKey(item.SeatId);
            if (seat != null)
            {
                _context.Entry(seat).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
                return seat;
                //_context.Update(item);
                //await _context.SaveChangesAsync(true);
                //return seat;
            }
            throw new NoSuchSeatFoundException();
        }
    }
}
