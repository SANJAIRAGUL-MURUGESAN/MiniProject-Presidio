using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Exceptions.TrackReservationExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class TrackReservationRepository : IRepository<int, TrackReservation>
    {
        private readonly RailwayReservationContext _context;
        public TrackReservationRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<TrackReservation> Add(TrackReservation item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TrackReservation> Delete(int key)
        {
            var TrackReservation = await GetbyKey(key);
            if (TrackReservation != null)
            {
                _context.Remove(TrackReservation);
                _context.SaveChangesAsync(true);
                return TrackReservation;
            }
            throw new NoSuchTrackReservationFoundException();
        }
        public async Task<TrackReservation> GetbyKey(int key)
        {
            var track = await _context.TrackReservations.FirstOrDefaultAsync(t => t.TrackReservationId == key);
            if (track != null)
            {
                return track;
            }
            throw new NoSuchTrackReservationFoundException();
        }
        public async Task<IEnumerable<TrackReservation>> Get()
        {
            var TrackReservations = await _context.TrackReservations.ToListAsync();
            if (TrackReservations != null)
            {
                return TrackReservations;
            }
            throw new NoTrackReservationsFoundException();
        }
        public async Task<TrackReservation> Update(TrackReservation item)
        {
            var TrackReservation = await GetbyKey(item.TrackReservationId);
            if (TrackReservation != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return TrackReservation;
            }
            throw new NoSuchTrackReservationFoundException();
        }
    }
}
