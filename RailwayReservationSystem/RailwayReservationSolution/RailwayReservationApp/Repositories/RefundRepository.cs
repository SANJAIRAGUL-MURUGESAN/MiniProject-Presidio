using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Exceptions.AdminExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using System.Diagnostics.CodeAnalysis;

namespace RailwayReservationApp.Repositories
{
    [ExcludeFromCodeCoverage]
    public class RefundRepository : IRepository<int, Refund>
    {
        private readonly RailwayReservationContext _context;
        public RefundRepository(RailwayReservationContext context)
        {
            _context = context;
        }
        public async Task<Refund> Add(Refund item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Refund> Delete(int key)
        {
            var refund = await GetbyKey(key);
            if (refund != null)
            {
                _context.Remove(refund);
                _context.SaveChangesAsync(true);
                return refund;
            }
            throw new NoSuchAdminFoundException();
        }

        public async Task<Refund> GetbyKey(int key)
        {
            var refund = await _context.Refunds.FirstOrDefaultAsync(t => t.RefundId == key);
            if (refund != null)
            {
                return refund;
            }
            throw new NoSuchAdminFoundException();
        }
        public async Task<IEnumerable<Refund>> Get()
        {
            var refunds = await _context.Refunds.ToListAsync();
            if (refunds != null)
            {
                return refunds;
            }
            throw new NoAdminsFoundException();
        }
        public async Task<Refund> Update(Refund item)
        {
            var refund = await GetbyKey(item.RefundId);
            if (refund != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return refund;
            }
            throw new NoSuchAdminFoundException();
        }
    }
}
