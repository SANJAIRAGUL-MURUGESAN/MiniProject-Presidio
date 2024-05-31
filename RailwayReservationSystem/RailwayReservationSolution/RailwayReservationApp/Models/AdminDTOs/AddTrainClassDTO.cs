namespace RailwayReservationApp.Models.AdminDTOs
{
    public class AddTrainClassDTO
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public float ClassPrice { get; set; }
        public int StartingSeatNumber { get; set; }
        public int EndingSeatNumber { get; set; }
        public int TrainId { get; set; }
    }
}
