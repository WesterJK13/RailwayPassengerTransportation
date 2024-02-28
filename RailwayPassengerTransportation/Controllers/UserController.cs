using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RailwayPassengerTransportation.Data;
using RailwayPassengerTransportation.Models;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

namespace RailwayPassengerTransportation.Controllers
{
    public class UserController : Controller
    {
        private readonly RailwayPassengerDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, RailwayPassengerDbContext railwayDbContext)
        {
            _context = railwayDbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Trips(DateTime timeStart, int stationFrom, int stationTo)
        {
             if (timeStart == null)
            {
                DateTime now = DateTime.Now;
                // получение доступных  listStations
                //List<ListStations> listStationsFrom = _context.listStations.Where(a => a.StationId==stationFrom).ToList();
               // List<ListStations> listStationsTo = _context.listStations.Where(a => a.StationId==stationTo).ToList();
                List<Trip> trips = _context.trips.Where(a => a.DateTimeLeave.Date == now.Date).ToList();

                List<ModelSearchTrip> modelSearchTrips = new List<ModelSearchTrip>();

                foreach (var item in trips)
                {
                    Models.Route route = _context.routes.Where(a => a.Id == item.Flight.RouteId).FirstOrDefault();
                    // первая станция для определения времени и цены
                    //ListStations firstStation = _context.listStations.Where(a => a.RouteId == route.Id).FirstOrDefault();

                    ListStations listStationFrom = _context.listStations.Where(a => a.RouteId == route.Id && a.StationId == stationFrom).FirstOrDefault();
                    ListStations listStationTo = _context.listStations.Where(a => a.RouteId == route.Id && a.StationId == stationTo).FirstOrDefault();

                    if (listStationFrom.Id >= listStationTo.Id) continue;

                    // высчитываем цену
                    double? cost = listStationTo.Cost-listStationFrom.Cost;
                    // подсчитываем время отправления
                    DateTime timeFrom = item.DateTimeLeave.AddDays(listStationFrom.TimeToArriveDay).AddHours(listStationFrom.TimeToArriveHour).AddMinutes(listStationFrom.TimeToArriveMinutes);
                    // время прибытия
                    DateTime timeTo = item.DateTimeLeave.AddDays(listStationTo.TimeToArriveDay).AddHours(listStationTo.TimeToArriveHour).AddMinutes(listStationTo.TimeToArriveMinutes);

                    ModelSearchTrip modelSearch = new ModelSearchTrip();
                    modelSearch.trip = item;
                    modelSearch.Cost = cost;
                   // modelSearch.stationsStart = firstStation;
                    modelSearch.stationsFrom = listStationFrom;
                    modelSearch.stationsTo = listStationTo;
                    modelSearch.DateTimeLeave = timeFrom;
                    modelSearch.DateTimeCome = timeTo;

                    modelSearchTrips.Add(modelSearch);

                }

                ViewData["trips"] = trips;
                ViewData["modelSearchTrips"] = modelSearchTrips;
            }
            else
            {
                List<Trip> trips = _context.trips.Where(a => a.DateTimeLeave.Date == timeStart.Date).ToList();

                List<ModelSearchTrip> modelSearchTrips = new List<ModelSearchTrip>();

                foreach (var item in trips)
                {
                    Flight? flight = _context.flights.Where(a=>a.Id==item.FlightId).FirstOrDefault();
                    Models.Route? route = _context.routes.Where(a => a.Id == flight.Id).FirstOrDefault();
                    // первая станция для определения времени и цены
                    //ListStations? firstStation = _context.listStations.Where(a => a.RouteId == route.Id).FirstOrDefault();

                    ListStations? listStationFrom = new ListStations();
                    ListStations? listStationTo = new ListStations();

                    if (route!=null && flight!=null)
                    {
                        listStationFrom = _context.listStations.Where(a => a.RouteId == route.Id && a.StationId == stationFrom).FirstOrDefault();
                        listStationTo = _context.listStations.Where(a => a.RouteId == route.Id && a.StationId == stationTo).FirstOrDefault();
                    }


                    if (listStationFrom==null || listStationTo==null || listStationFrom.Id >= listStationTo.Id) continue;

                    // высчитываем цену
                    double? cost = listStationTo.Cost - listStationFrom.Cost;
                    // подсчитываем время отправления
                    DateTime timeFrom = item.DateTimeLeave.AddDays(listStationFrom.TimeToArriveDay).AddHours(listStationFrom.TimeToArriveHour).AddMinutes(listStationFrom.TimeToArriveMinutes);
                    // время прибытия
                    DateTime timeTo = item.DateTimeLeave.AddDays(listStationTo.TimeToArriveDay).AddHours(listStationTo.TimeToArriveHour).AddMinutes(listStationTo.TimeToArriveMinutes);

                    ModelSearchTrip modelSearch = new ModelSearchTrip();
                    modelSearch.trip = item;
                    modelSearch.Cost = cost;
                    //modelSearch.stationsStart = firstStation;
                    modelSearch.stationsFrom = listStationFrom;
                    modelSearch.stationsTo = listStationTo;
                    modelSearch.DateTimeLeave = timeFrom;
                    modelSearch.DateTimeCome = timeTo;

                    modelSearchTrips.Add(modelSearch);

                }

                ViewData["trips"] = trips;
                ViewData["modelSearchTrips"] = modelSearchTrips;
            }
            List<Models.Route> routes = _context.routes.ToList();
            ViewData["routes"] = routes;

            List<Flight> flights = _context.flights.ToList();
            ViewData["flights"] = flights;

            List<Train> trains = _context.trains.ToList();
            ViewData["trains"] = trains;

            List<RailwayCarriage> railwayCarriages = _context.railwayCarriages.ToList();
            ViewData["railwayCarriages"] = railwayCarriages;

            List<Ticket> tickets = _context.tickets.ToList();
            ViewData["tickets"] = tickets;

            List<Station> stations= _context.stations.ToList();
            ViewData["stations"] = stations;

            List<ListStations> listStations= _context.listStations.ToList();
            ViewData["listStations"] = listStations;

            //ViewData["listStations"] = stationFrom;
            //ViewData["listStations"] = stationTo;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChooseDateTrip(DateTime TimeStart,int stationFrom,int stationTo)
        {
            //List<Flight> flights = _context.flights.Where(a => a.RouteId == route).ToList();
            //ViewData["flights"] = flights;

            //ViewData["routeChoose"] = routeObj;

            return RedirectToAction("Trips", "User", new { timeStart = TimeStart,stationFrom = stationFrom,stationTo=stationTo });
        }

        public async Task<IActionResult> BuyTicket(int id, double? cost,int trip,int start,int end,DateTime timeCome, DateTime timeLeave)
        {

            Ticket ticket = new Ticket();
            ticket.Cost = cost;
            ticket.StationStartId = start;
            ticket.StationEndId = end;
            ticket.TripId = trip;
            ticket.DateTimeStart= timeLeave;
            ticket.DateTimeEnd= timeCome;

            _context.tickets.Add(ticket);

            // списывание денег с user
            Passenger passenger = new Passenger();
            passenger.Id = Convert.ToInt32(Request.Cookies["userId"]);
            passenger.Email = _context.passengers.Where(x => x.Id==passenger.Id).Select(x => x.Email).First();
            passenger.Password = _context.passengers.Where(x => x.Id==passenger.Id).Select(x => x.Password).First();
            passenger.Fathername = _context.passengers.Where(x => x.Id==passenger.Id).Select(x => x.Fathername).First();
            passenger.Surname = _context.passengers.Where(x => x.Id==passenger.Id).Select(x => x.Surname).First();
            passenger.Name = _context.passengers.Where(x => x.Id==passenger.Id).Select(x => x.Name).First();
            passenger.Passport = _context.passengers.Where(x => x.Id==passenger.Id).Select(x => x.Passport).First();
            passenger.Money = _context.passengers.Where(a=>a.Id==Convert.ToInt32(Request.Cookies["userId"])).Select(s=>s.Money).FirstOrDefault()-cost;
            _context.passengers.Update(passenger);
            await _context.SaveChangesAsync();
            return Redirect("~/User/Trips");
        }

        public IActionResult Tickets()
        {
            List<Ticket> tickets= _context.tickets.ToList();
            ViewData["tickets"] = tickets;

            List<Station> stations= _context.stations.ToList();
            ViewData["stations"] = stations;

            List<ListStations> listStations= _context.listStations.ToList();
            ViewData["listStations"] = listStations;

            return View();
        }

        public IActionResult Balance()
        {
            Passenger passengers= _context.passengers.Where(a=>a.Id==Convert.ToInt32(Request.Cookies["userId"])).FirstOrDefault();
            ViewData["passengers"] = passengers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMoney(int id,string Money)
        {
            Passenger passenger = new Passenger();
            passenger.Id = id;
            passenger.Email = _context.passengers.Where(x => x.Id == passenger.Id).Select(x => x.Email).First();
            passenger.Password = _context.passengers.Where(x => x.Id == passenger.Id).Select(x => x.Password).First();
            passenger.Fathername = _context.passengers.Where(x => x.Id == passenger.Id).Select(x => x.Fathername).First();
            passenger.Surname = _context.passengers.Where(x => x.Id == passenger.Id).Select(x => x.Surname).First();
            passenger.Name = _context.passengers.Where(x => x.Id == passenger.Id).Select(x => x.Name).First();
            passenger.Passport = _context.passengers.Where(x => x.Id == passenger.Id).Select(x => x.Passport).First();
            passenger.Money = _context.passengers.Where(a => a.Id == Convert.ToInt32(Request.Cookies["userId"])).Select(s => s.Money).FirstOrDefault() + Convert.ToInt32(Money);
            _context.passengers.Update(passenger);
            await _context.SaveChangesAsync();
            return Redirect("~/User/Balance");
        }
    }
}