using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.RewardExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class RewardRepository : IRepository<int, Rewards>
    {
        private readonly RailwayReservationContext _context;
        public RewardRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Rewards> Add(Rewards item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Rewards> Delete(int key)
        {
            var reward = await GetbyKey(key);
            if (reward != null)
            {
                _context.Remove(reward);
                _context.SaveChangesAsync(true);
                return reward;
            }
            throw new NoSuchRewardFoundException();
        }
        public async Task<Rewards> GetbyKey(int key)
        {
            var reward = await _context.Rewards.FirstOrDefaultAsync(t => t.RewardId == key);
            if(reward != null)
            {
                return reward;
            }
            throw new NoSuchRewardFoundException();
        }
        public async Task<IEnumerable<Rewards>> Get()
        {
            var rewards = await _context.Rewards.ToListAsync();
            if (rewards != null)
            {
                return rewards;
            }
            throw new NoSuchRewardFoundException();
        }
        public async Task<Rewards> Update(Rewards item)
        {
            var reward = await GetbyKey(item.RewardId);
            if (reward != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return reward;
            }
            throw new NoSuchRewardFoundException();
        }
    }
}
