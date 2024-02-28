namespace RailwayPassengerTransportation.Models
{
    public class RailwayCarriage
    {
        // вагон поезда
        public int Id { get; set; }
        public int Num { get; set; }
        public int TrainId { get; set; }
        public Train? Train { get; set; }
        // кол-во мест
        public int CountSeats { get; set; }
    }
}
