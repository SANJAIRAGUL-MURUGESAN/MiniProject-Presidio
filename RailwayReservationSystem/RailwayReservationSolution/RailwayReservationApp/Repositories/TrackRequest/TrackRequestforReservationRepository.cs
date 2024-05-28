using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories.TrackRequest
{
    public class TrackRequestforReservationRepository : TrackRepository
    {
        public TrackRequestforReservationRepository(RailwayReservationContext context) : base(context)
        {
        }
        public async override Task<Track> GetbyKey(int key)
        {
            var classes = _context.Tracks.Include(e => e.TrackReservations).SingleOrDefault(e => e.TrackId == key);
            return classes;
        }
    }
}
