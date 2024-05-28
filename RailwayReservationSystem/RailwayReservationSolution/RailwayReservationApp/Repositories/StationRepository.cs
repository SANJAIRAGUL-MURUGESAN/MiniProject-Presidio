using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.StationExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class StationRepository : IRepository<int, Station>
    {
        protected readonly RailwayReservationContext _context;
        public StationRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Station> Add(Station item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Station> Delete(int key)
        {
            var station = await GetbyKey(key);
            if (station != null)
            {
                _context.Remove(station);
                _context.SaveChangesAsync(true);
                return station;
            }
            throw new NoSuchStationFoundException();
        }
        public virtual Task<Station> GetbyKey(int key)
        {
            var station = _context.Stations.FirstOrDefaultAsync(t => t.StationId == key);
            if (station != null)
            {
                return station;
            }
            throw new NoSuchStationFoundException();
        }
        public async Task<IEnumerable<Station>> Get()
        {
            var stations = await _context.Stations.ToListAsync();
            if (stations != null)
            {
                return stations;
            }
            throw new NoStationsFoundException();
        }
        public async Task<Station> Update(Station item)
        {
            var station = await GetbyKey(item.StationId);
            if (station != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return station;
            }
            throw new NoSuchStationFoundException();
        }
    }
}
