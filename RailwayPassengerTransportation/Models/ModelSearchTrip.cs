namespace RailwayPassengerTransportation.Models
{
    public class ModelSearchTrip
    {
        public int Id { get; set; }
        // дата отправления
        public DateTime DateTimeLeave { get; set; }
        // дата прибытия
        public DateTime DateTimeCome { get; set; }
        public ListStations stationsFrom { get; set; }
        public ListStations stationsTo { get; set; }
        public ListStations stationsStart { get; set; }
        public Trip trip { get; set; }
        public double? Cost { get; set; }
    }
}
