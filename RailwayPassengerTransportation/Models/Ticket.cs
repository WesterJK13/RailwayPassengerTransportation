namespace RailwayPassengerTransportation.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public double? Cost { get; set; }
        public int StationStartId { get; set; }
        public int StationEndId { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        // время отправления и время прибытия
        public DateTime? DateTimeStart { get; set; }
        public DateTime? DateTimeEnd { get; set; }
    }
}
