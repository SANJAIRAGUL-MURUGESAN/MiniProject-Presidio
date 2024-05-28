using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories.StationRequest
{
    public class StationRequestRepository : StationRepository
    {
        public StationRequestRepository(RailwayReservationContext context) : base(context)
        {
        }
        public async override Task<Station> GetbyKey(int key)
        {
            var station = _context.Stations.Include(e => e.Tracks).SingleOrDefault(e => e.StationId == key);
            return station;
        }
    }
}
