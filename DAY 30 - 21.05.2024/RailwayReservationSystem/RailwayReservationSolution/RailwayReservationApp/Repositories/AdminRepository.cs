using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories
{
    public class AdminRepository : IRepository<int, Admin>
    {
        private readonly RailwayReservationContext _context;
        public AdminRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Admin> Add(Admin item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Admin> Delete(int key)
        {
            var admin = await GetbyKey(key);
            if (admin != null)
            {
                _context.Remove(admin);
                _context.SaveChangesAsync(true);
                return admin;
            }
            throw new NoSuchAdminFoundException();
        }
        public Task<Admin> GetbyKey(int key)
        {
            var admin = _context.Admins.FirstOrDefaultAsync(t => t.Id == key);
            if (admin != null)
            {
                return admin;
            }
            throw new NoSuchAdminFoundException();
        }
        public async Task<IEnumerable<Admin>> Get()
        {
            var admins = await _context.Admins.ToListAsync();
            if (admins != null)
            {
                return admins;
            }
            throw new NoAdminsFoundException();
        }
        public async Task<Admin> Update(Admin item)
        {
            var user = await GetbyKey(item.Id);
            if (user != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return user;
            }
            throw new NoSuchAdminFoundException();
        }
    }
}
