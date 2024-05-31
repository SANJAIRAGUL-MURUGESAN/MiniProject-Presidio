using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.TrainClassExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class TrainClassRepository : IRepository<int, TrainClass>
    {
        private readonly RailwayReservationContext _context;
        public TrainClassRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<TrainClass> Add(TrainClass item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TrainClass> Delete(int key)
        {
            var trainClass = await GetbyKey(key);
            if (trainClass != null)
            {
                _context.Remove(trainClass);
                _context.SaveChangesAsync(true);
                return trainClass;
            }
            throw new NoSuchTrainClassFoundException();
        }
        public async Task<TrainClass> GetbyKey(int key)
        {
            var trainClass = await _context.TrainClasses.FirstOrDefaultAsync(t => t.TrainId == key);
            if (trainClass != null)
            {
                return trainClass;
            }
            throw new NoSuchTrainClassFoundException();
        }
        public async Task<IEnumerable<TrainClass>> Get()
        {
            var trainsClasses = await _context.TrainClasses.ToListAsync();
            if (trainsClasses != null)
            {
                return trainsClasses;
            }
            throw new NoTrainClassFoundException();
        }
        public async Task<TrainClass> Update(TrainClass item)
        {
            var trainClass = await GetbyKey(item.TrainId);
            if (trainClass != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return trainClass;
            }
            throw new NoSuchTrainClassFoundException();
        }
    }
}
