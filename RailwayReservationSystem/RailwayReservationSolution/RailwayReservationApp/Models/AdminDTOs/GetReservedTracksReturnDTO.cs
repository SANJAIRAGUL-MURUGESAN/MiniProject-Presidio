namespace RailwayReservationApp.Models.AdminDTOs
{
    public class GetReservedTracksReturnDTO
    {
        public List<int> ReservedTracks { get; set; }
        public List<int> AvailableTracks { get; set; }
    }
}
