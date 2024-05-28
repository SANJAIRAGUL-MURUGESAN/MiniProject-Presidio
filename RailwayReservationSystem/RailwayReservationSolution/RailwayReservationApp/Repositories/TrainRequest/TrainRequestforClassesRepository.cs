using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Repositories.TrainRequest
{
    public class TrainRequestforClassesRepository : TrainRepository
    {
        public TrainRequestforClassesRepository(RailwayReservationContext context) : base(context)
        {
        }
        public async override Task<Train> GetbyKey(int key)
        {
            var classes = _context.Trains.Include(e => e.TrainClasses).SingleOrDefault(e => e.TrainId == key);
            return classes;
        }
    }
}
