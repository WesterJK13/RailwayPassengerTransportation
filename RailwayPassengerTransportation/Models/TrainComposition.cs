namespace RailwayPassengerTransportation.Models
{
    public class TrainComposition
    {
        // состав персонала
        public int Id { get; set; }
        public int MachinistId { get; set; }
        public Machinist? Machinist { get; set; }
        public int ConductorId { get; set; }
        public Conductor? Conductor { get; set; }
    }
}
