using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories.ReservationRequest
{
    public class ReservationRequestforSeatsRepository : ReservationRepository
    {
        public ReservationRequestforSeatsRepository(RailwayReservationContext context) : base(context)
        {
        }
        public async override Task<Reservation> GetbyKey(int key)
        {
            var Seats = _context.Reservations.Include(e => e.Seats).SingleOrDefault(e => e.ReservationId == key);
            return Seats;
        }
    }
}
