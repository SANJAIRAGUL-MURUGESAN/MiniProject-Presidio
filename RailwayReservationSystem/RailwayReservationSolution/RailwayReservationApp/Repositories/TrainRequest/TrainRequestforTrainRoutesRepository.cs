using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories.TrainRequest
{
    public class TrainRequestforTrainRoutesRepository : TrainRepository
    {
        public TrainRequestforTrainRoutesRepository(RailwayReservationContext context) : base(context)
        {
        }
        public async override Task<Train> GetbyKey(int key)
        {
            var Routes = _context.Trains.Include(e => e.TrainRoutes).SingleOrDefault(e => e.TrainId == key);
            return Routes;
        }
    }
}
