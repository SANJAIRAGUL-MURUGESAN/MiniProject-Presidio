namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddTrainClassReturnDTO
    {
        public string ClassName { get; set; }
        public string ClassPrice { get; set; }
        public int StartingSeatNumber { get; set; }
        public int EndingSeatNumber { get; set; }
        public int TrainId { get; set; }
    }
}
