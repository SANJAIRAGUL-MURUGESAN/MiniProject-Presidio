using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class UserRepository : IRepository<int, Users>
    {
        private readonly RailwayReservationContext _context;
        public UserRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Users> Add(Users item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Users> Delete(int key)
        {
            var user = await GetbyKey(key);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChangesAsync(true);
                return user;
            }
            throw new NoSuchUserFoundException();
        }
        public async Task<Users> GetbyKey(int key)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == key);
            if (user != null)
            {
                return user;
            }
            throw new NoSuchUserFoundException();
        }
        public async Task<IEnumerable<Users>> Get()
        {
            var users = await _context.Users.ToListAsync();
            if (users != null)
            {
                return users;
            }
            throw new NoUserFoundException();
        }
        public async Task<Users> Update(Users item)
        {
            var user = await GetbyKey(item.Id);
            if (user != null)
            {
                _context.Entry(user).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
                //_context.Update(item);
                //await _context.SaveChangesAsync(true);
                //return user;
                //_context.Entry<Users>(item).State = EntityState.Modified;
                //await _context.SaveChangesAsync();
            }
            return user;
            throw new NoSuchUserFoundException();
        }
    }
}
