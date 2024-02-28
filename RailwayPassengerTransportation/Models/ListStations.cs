namespace RailwayPassengerTransportation.Models
{
    public class ListStations
    {
        // список станций внутри маршрута
        public int Id { get; set; }
        // порядковый номер
        public int? OrderNum { get; set; }
        //является ли начальной станцией
        public Boolean isStart { get; set; }
        // время затраченное на достижение данной остановки (в виде числа timestamp, чтобы потом конвертировать в дату)
        public string? TimeToArrive { get; set; }
        // время затраченное на остановку на данной станции (в виде числа timestamp, чтобы потом конвертировать в дату)
        public string? TimeToLeave { get; set; }
        // цена до данной станции относительно начальной станции
        public double? Cost { get; set; }
        public int? StationId { get; set; }
        public Station? Station { get; set; }
        public int? RouteId { get; set; }
        public Route? Route { get; set; }

        public bool isActive { get; set; }

        // время
        public int TimeToArriveDay { get; set; }
        public int TimeToArriveHour { get; set; }
        public int TimeToArriveMinutes { get; set; }
        public int TimeToLeaveMinutes { get;set; }
    }
}
