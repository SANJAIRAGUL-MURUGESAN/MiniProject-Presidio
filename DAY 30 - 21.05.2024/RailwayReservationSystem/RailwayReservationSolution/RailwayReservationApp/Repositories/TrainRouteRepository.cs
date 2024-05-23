using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.TrainRoutesExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class TrainRouteRepository : IRepository<int, TrainRoutes>
    {
        private readonly RailwayReservationContext _context;
        public TrainRouteRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<TrainRoutes> Add(TrainRoutes item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TrainRoutes> Delete(int key)
        {
            var trainRoutes = await GetbyKey(key);
            if (trainRoutes != null)
            {
                _context.Remove(trainRoutes);
                _context.SaveChangesAsync(true);
                return trainRoutes;
            }
            throw new NoSuchTrainRouteFoundException();
        }
        public Task<TrainRoutes> GetbyKey(int key)
        {
            var trainRoutes = _context.TrainRoutes.FirstOrDefaultAsync(t => t.RouteId == key);
            if (trainRoutes != null) 
            {
                return trainRoutes;
            }
            throw new NoSuchTrainRouteFoundException();
        }
        public async Task<IEnumerable<TrainRoutes>> Get()
        {
            var trainRoutes = await _context.TrainRoutes.ToListAsync();
            if(trainRoutes != null)
            {
                return trainRoutes;
            }
            throw new NoTrainRoutesFoundException();
        }
        public async Task<TrainRoutes> Update(TrainRoutes item)
        {
            var employee = await GetbyKey(item.RouteId);
            if (employee != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return employee;
            }
            throw new NoTrainRoutesFoundException();
        }
    }
}
