namespace RailwayReservationApp.Models.UserDTOs
{
    public class GetTrainClassReturnDTO
    {
        public string ClassName { get; set; }
        public int StartingSeatNumber { get; set; }
        public int EndingSeatNumber { get; set; }
        public float ClassPrice { get; set; }
    }
}
