using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class TrackRepository : IRepository<int, Track>
    {
        protected readonly RailwayReservationContext _context;
        public TrackRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Track> Add(Track item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Track> Delete(int key)
        {
            var track = await GetbyKey(key);
            if (track != null)
            {
                _context.Remove(track);
                _context.SaveChangesAsync(true);
                return track;
            }
            throw new NoSuchTrackFoundException();
        }
        public virtual Task<Track> GetbyKey(int key)
        {
            var track = _context.Tracks.FirstOrDefaultAsync(t => t.TrackId == key);
            if (track != null)
            {
                return track;
            }
            throw new NoSuchReservationFoundException();
        }
        public async Task<IEnumerable<Track>> Get()
        {
            var tracks = await _context.Tracks.ToListAsync();
            if (tracks != null)
            {
                return tracks;
            }
            throw new NoReservationsFoundException();
        }
        public async Task<Track> Update(Track item)
        {
            var track = await GetbyKey(item.TrackId);
            if (track != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return track;
            }
            throw new NoSuchReservationFoundException();
        }
    }
}
