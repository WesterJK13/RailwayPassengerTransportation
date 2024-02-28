namespace RailwayPassengerTransportation.Models
{
    public class GetListStationsJson
    {
        // время затраченное на достижение данной остановки (в виде числа timestamp, чтобы потом конвертировать в дату)
        public string timetoarriveminutes { get; set; }
        // время затраченное на остановку на данной станции (в виде числа timestamp, чтобы потом конвертировать в дату)
        public string timetoarrivehour { get; set; }
        // цена до данной станции относительно начальной станции
        public string timetoarriveday { get; set; }
        public string station { get; set; }
        public string timetoleave { get; set; }
        public string cost { get; set; }
    }
}
