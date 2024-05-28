using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories.TrainRequest
{
    public class TrainRequestforReservationsRepository : TrainRepository
    {
        public TrainRequestforReservationsRepository(RailwayReservationContext context) : base(context)
        {
        }
        public async override Task<Train> GetbyKey(int key)
        {
            var Reservations = _context.Trains.Include(e => e.TrainReservations).SingleOrDefault(e => e.TrainId == key);
            return Reservations;
        }
    }
}
