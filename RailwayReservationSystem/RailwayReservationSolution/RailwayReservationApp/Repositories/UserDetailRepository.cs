using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class UserDetailRepository : IRepository<int, UserDetails>
    {
        private readonly RailwayReservationContext _context;
        public UserDetailRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<UserDetails> Add(UserDetails item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<UserDetails> Delete(int key)
        {
            var userDetail = await GetbyKey(key);
            if (userDetail != null)
            {
                _context.Remove(userDetail);
                _context.SaveChangesAsync(true);
                return userDetail;
            }
            throw new NoSuchUserFoundException();
        }
        public Task<UserDetails> GetbyKey(int key)
        {
            var userDetail = _context.UserDetails.FirstOrDefaultAsync(t => t.UserId == key);
            if (userDetail != null)
            {
                return userDetail;
            }
            throw new NoSuchUserFoundException();
        }
        public async Task<IEnumerable<UserDetails>> Get()
        {
            var UserDetails = await _context.UserDetails.ToListAsync();
            if (UserDetails != null)
            {
                return UserDetails;
            }
            throw new NoUserFoundException();
        }
        public async Task<UserDetails> Update(UserDetails item)
        {
            var UserDetail = await GetbyKey(item.UserId);
            if (UserDetail != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return UserDetail;
            }
            throw new NoSuchUserFoundException();
        }
    }
}
