namespace RailwayPassengerTransportation.Models
{
    public class Flight
    {
        public int Id { get; set; }
        // маршрут id
        public int RouteId { get; set; }
        public Route? Route { get; set; }
        // время отправления поезда (отсюда берется только время, без даты)
        public DateTime TimeStart { get; set; }
        // дни недели в которые работает рейс
        public bool isMon {  get; set; }
        public bool isThues {  get; set; }
        public bool isWen {  get; set; }
        public bool isThurs {  get; set; }
        public bool isFrid {  get; set; }
        public bool isSat {  get; set; }
        public bool isSun {  get; set; }
        //public List<ListDayWeeks>? ListDayWeeks { get; set; } = new List<ListDayWeeks>();
        //public List<DayOfWeek>? DayOfWeeks { get; set; }
        public int TrainId { get; set; }
        public Train? Train { get; set; }

        public int ConductorId { get; set; }
        public Conductor? Conductor { get; set; }

        public int MachinistId { get; set; }
        public Machinist? Machinist { get; set; }
        //public TrainComposition? TrainComposition { get; set; }
    }
}
