using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class TrainRepository : IRepository<int, Train>
    {
        protected readonly RailwayReservationContext _context;
        public TrainRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Train> Add(Train item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Train> Delete(int key)
        {
            var train = await GetbyKey(key);
            if (train != null)
            {
                _context.Remove(train);
                _context.SaveChangesAsync(true);
                return train;
            }
            throw new NoSuchTrainFoundException();
        }
        public async virtual Task<Train> GetbyKey(int key)
        {
            var train = await _context.Trains.FirstOrDefaultAsync(t => t.TrainId == key);
            if (train != null)
            {
                return train;
            }
            throw new NoSuchTrainFoundException();
        }
        public async Task<IEnumerable<Train>> Get()
        {
            var trains = await _context.Trains.ToListAsync();
            if (trains != null)
            {
                return trains;
            }
            throw new NoTrainsFoundException();
        }
        public async Task<Train> Update(Train item)
        {
            var employee = await GetbyKey(item.TrainId);
            if (employee != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return employee;
            }
            throw new NoSuchTrainFoundException();
        }


    }
}
