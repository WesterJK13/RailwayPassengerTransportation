using Microsoft.AspNetCore.Mvc;
using RailwayPassengerTransportation.Data;
using RailwayPassengerTransportation.Models;
using System.Diagnostics;

namespace RailwayPassengerTransportation.Controllers
{
    public class HomeController : Controller
    {
        private readonly RailwayPassengerDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RailwayPassengerDbContext railwayDbContext)
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

        [HttpPost]
        public async Task<ActionResult> Login(string Email, string Password)
        {
            if (_context.passengers.Any(a => a.Password == Password && a.Email == Email))
            {
                int id = _context.passengers.Where(a => a.Password == Password && a.Email == Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "User", new { id = id });
            }
            else if (_context.machinists.Any(a => a.Password == Password && a.Email == Email))
            {
                int id = _context.machinists.Where(a => a.Password == Password && a.Email == Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "Machinist", new { id = id });
            }
            else if (_context.conductors.Any(a => a.Password == Password && a.Email == Email))
            {
                int id = _context.conductors.Where(a => a.Password == Password && a.Email == Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "Conductor", new { id = id });
            }
            else if (_context.dispatchers.Any(a => a.Password == Password && a.Email == Email))
            {
                int id = _context.dispatchers.Where(a => a.Password == Password && a.Email == Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "Dispatcher", new { id = id });
            }
            else if (_context.admins.Any(a => a.Password == Password && a.Email == Email))
            {
                int id = _context.admins.Where(a => a.Password == Password && a.Email == Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "Admin", new { id = id });
            }
            return Unauthorized();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("userId");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(string Surname, string Name, string FatherName, string Email, string Password,string Passport)
        {
            Passenger passenger= new Passenger();
            passenger.Email = Email;
            passenger.Password = Password;
            passenger.Surname = Surname;
            passenger.Name = Name;
            passenger.Fathername = FatherName;
            passenger.Passport = Passport;
            passenger.Money = 0;
            _context.passengers.Add(passenger);

            _context.SaveChanges();
            int id = _context.passengers.Where(a => a.Password == Password && a.Email == Email).Select(a => a.Id).FirstOrDefault();

            Response.Cookies.Append("userId", id.ToString());
            return RedirectToAction("Index", "User", new { id = id });
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
                    ListStations firstStation = _context.listStations.Where(a => a.RouteId == route.Id).FirstOrDefault();

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
                    modelSearch.stationsStart = firstStation;
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
                    ListStations? firstStation = _context.listStations.Where(a => a.RouteId == route.Id).FirstOrDefault();

                    ListStations? listStationFrom = _context.listStations.Where(a => a.RouteId == route.Id && a.StationId == stationFrom).FirstOrDefault();
                    ListStations? listStationTo = _context.listStations.Where(a => a.RouteId == route.Id && a.StationId == stationTo).FirstOrDefault();

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
                    modelSearch.stationsStart = firstStation;
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

            return RedirectToAction("Trips", "Home", new { timeStart = TimeStart,stationFrom = stationFrom,stationTo=stationTo });
        }
    }
}