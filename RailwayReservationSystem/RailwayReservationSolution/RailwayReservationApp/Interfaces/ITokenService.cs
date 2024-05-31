using RailwayReservationApp.Models;

namespace RailwayReservationApp.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(Users user);
        public Task<string> GenerateTokenAdmin(Admin admin);
    }
}
