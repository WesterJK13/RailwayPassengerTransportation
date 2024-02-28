namespace RailwayPassengerTransportation.Models
{
    public class Trip
    {
        public int Id { get; set; }
        // дата отправления
        public DateTime DateTimeLeave { get; set; }
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
    }
}
