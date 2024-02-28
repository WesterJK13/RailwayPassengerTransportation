using Microsoft.AspNetCore.Mvc;
using RailwayPassengerTransportation.Data;
using RailwayPassengerTransportation.Models;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace RailwayPassengerTransportation.Controllers
{
    public class DispatcherController : Controller
    {
        private readonly RailwayPassengerDbContext _context;
        private readonly ILogger<DispatcherController> _logger;

        public DispatcherController(ILogger<DispatcherController> logger, RailwayPassengerDbContext railwayDbContext)
        {
            _context = railwayDbContext;
            _logger = logger;
        }

        public IActionResult Index()
        { //список созданных им курсов

            return View();
        }

        public IActionResult Stations()
        {
            List<Station> stations = _context.stations.ToList();
            ViewData["stations"] = stations;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStation(string Name)
        {
            Station station = new Station();
            station.Name = Name;
            _context.stations.Add(station);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Stations");
        }

        public async Task<IActionResult> DeleteStation(int id)
        {
            _context.stations.Remove(_context.stations.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Stations");
        }

        [HttpPost]
        public async Task<IActionResult> EditStation(int id, string Name)
        {
            Station station= new Station();
            station.Name= Name;
            station.Id = id;

            _context.stations.Update(station);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Stations");
        }

        [HttpPost]
        public async Task<IActionResult> EditRoute(int id, string Name)
        {
            Models.Route route= new Models.Route();
            route.Name = Name;
            route.Id = id;

            _context.routes.Update(route);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Routes");
        }

        [HttpPost]
        public async Task<IActionResult> AddRoute(string Name)
        {
            Models.Route route= new Models.Route();
            //var json = JsonConvert.DeserializeObject(arr);
            //if (json!=null)
            //{

            //    json.GetType();
            //}
            route.Name = Name;
            _context.routes.Add(route);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Routes");
        }

        [HttpPost]
        public async Task<IActionResult> AddStationToRoute(string idRoute, string station, string TimeToArriveDay,string TimeToArriveHour, string TimeToArriveMinutes, string TimeToLeave, string Cost)
        {
            ListStations listStations= new ListStations();

            listStations.RouteId = Convert.ToInt32(idRoute);

            listStations.StationId= Convert.ToInt32(station);

            listStations.Cost = Convert.ToDouble(Cost);
            //DateTime TimeToArrive = new DateTime();
            //DateTime TimeToLeaveDate = new DateTime();
            //TimeToArrive.AddDays(Convert.ToDouble(TimeToArriveDay));
            //TimeToArrive.AddHours(Convert.ToDouble(TimeToArriveHour));
            //TimeToArrive.AddMinutes(Convert.ToDouble(TimeToArriveMinutes));
            //TimeToLeaveDate.AddDays(Convert.ToDouble(TimeToLeave));
            // получение timestamp
            //listStations.TimeToArrive= TimeToArrive.ToString("yyyyMMddHHmmssffff");
            //listStations.TimeToLeave= TimeToLeaveDate.ToString("yyyyMMddHHmmssffff");

            listStations.TimeToArriveDay = Convert.ToInt32(TimeToArriveDay);
            listStations.TimeToArriveHour = Convert.ToInt32(TimeToArriveHour);
            listStations.TimeToArriveMinutes = Convert.ToInt32(TimeToArriveMinutes);
            listStations.TimeToLeaveMinutes = Convert.ToInt32(TimeToLeave);
            _context.listStations.Add(listStations);
            await _context.SaveChangesAsync();
            return RedirectToAction("AddRoute", "Dispatcher", new { id = idRoute });
        }

        public async Task<IActionResult> DeleteRoute(int id)
        {
            //  удаление связанных списков станций
            

            _context.routes.Remove(_context.routes.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Routes");
        }

        public async Task<IActionResult> DeleteStationInRoute(int id,int IdRoute)
        {
            _context.listStations.Remove(_context.listStations.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return RedirectToAction("AddRoute", "Dispatcher", new { id = IdRoute });
        }

        public IActionResult Routes()
        {
            List<Models.Route> routes= _context.routes.ToList();
            ViewData["routes"] = routes;
            return View();
        }

        public IActionResult AddRoute(int id)
        {   
            // получение списка станций
            List<Station> stations = _context.stations.ToList();
            ViewData["stations"] = stations;

            List<ListStations> listStations= _context.listStations.Where(a=>a.Route.Id==id).ToList();
            //listStations.ForEach(s =>
            //{
            //    s.station = stations.Where(a=>a.Id==s.station);
            //});
            ViewData["listStations"] = listStations;
            ViewData["routeId"] = id;

            return View();
        }

        public IActionResult AddFlight(int id)
        {
            List<Models.Route> routes = _context.routes.ToList();
            ViewData["routes"] = routes;
            List<Machinist> machinists = _context.machinists.ToList();
            ViewData["machinists"] = machinists;
            List<Conductor> conductors= _context.conductors.ToList();
            ViewData["conductors"] = conductors;
            List<Train> trains = _context.trains.ToList();
            ViewData["trains"] = trains;
            List<Trip> trips = _context.trips.ToList();
            ViewData["trips"] = trips;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight(int route, string isMon, string isThues, string isWen, string isThurs, string isFrid, string isSat, string isSun, DateTime TimeStart,int train,int machinist, int conductor)
        {
            Flight flight= new Flight();

            flight.TrainId = Convert.ToInt32(train);
            flight.ConductorId = Convert.ToInt32(conductor);
            flight.MachinistId = Convert.ToInt32(machinist);
            flight.RouteId= Convert.ToInt32(route);
            if (isMon!=null) flight.isMon = true;
            if (isThues != null) flight.isThues = true;
            if (isWen != null) flight.isWen = true;
            if (isThurs != null) flight.isThurs = true;
            if (isFrid != null) flight.isFrid = true;
            if (isSat != null) flight.isSat = true;
            if (isSun != null) flight.isSun = true;
            //flight.isThues = Convert.ToBoolean(isThues); flight.isWen = Convert.ToBoolean(isWen); flight.isThurs = Convert.ToBoolean(isThurs); flight.isFrid = Convert.ToBoolean(isFrid); flight.isSat = Convert.ToBoolean(isSat); flight.isSun = Convert.ToBoolean(isSun);
            flight.TimeStart = TimeStart;
            //DateTime now = DateTime.Now;
            // формирование поездок по данному рейсу в зависимости от выбранных дней
            //while (TimeStart.Year==now.Year)
            //{
            //    Trip trip = new Trip();
            //    if (TimeStart.DayOfWeek == DayOfWeek.Monday && isMon)
            //    {
            //        //trip.FlightId = 
            //    }
            //    TimeStart.AddDays(1);
            //}

            //return Ok(TimeStart);

            _context.flights.Add(flight);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/ListFlights");
        }



        public IActionResult ListFlights(int id)
        {
            List<Models.Route> routes = _context.routes.ToList();
            ViewData["routes"] = routes;

            List<Flight> flights= _context.flights.Where(a=>a.RouteId==id).ToList();
            ViewData["flights"] = flights;

            List<Train> trains= _context.trains.ToList();
            ViewData["trains"] = trains;

            List<Machinist> machinists = _context.machinists.ToList();
            ViewData["machinists"] = machinists;

            List<Conductor> conductors = _context.conductors.ToList();
            ViewData["conductors"] = conductors;

            List<Trip> trips = _context.trips.ToList();
            ViewData["trips"] = trips;

            return View();
        }

        public async Task<IActionResult> createTrips(int id)
        {
            Flight flight = _context.flights.Where(a=> a.Id==id).FirstOrDefault();

            DateTime now = DateTime.Now;
            DateTime TimeStart = flight.TimeStart;
            // формирование поездок по данному рейсу в зависимости от выбранных дней
            while (TimeStart.Year == now.Year)
            {
                Trip trip = new Trip();
                if ((TimeStart.DayOfWeek == DayOfWeek.Monday && flight.isMon) || (TimeStart.DayOfWeek == DayOfWeek.Tuesday && flight.isThues)
                    || (TimeStart.DayOfWeek == DayOfWeek.Wednesday && flight.isWen) || (TimeStart.DayOfWeek == DayOfWeek.Thursday && flight.isThurs)
                    || (TimeStart.DayOfWeek == DayOfWeek.Friday && flight.isFrid) || (TimeStart.DayOfWeek == DayOfWeek.Saturday && flight.isSat)
                    || (TimeStart.DayOfWeek == DayOfWeek.Sunday && flight.isSun))
                {
                    trip.FlightId = flight.Id;
                    trip.DateTimeLeave = TimeStart;

                    _context.trips.Add(trip);
                    await _context.SaveChangesAsync();
                }
                TimeStart=TimeStart.AddDays(1);
            }

            //_context.trains.Remove(_context.trains.Where(a => a.Id == id).FirstOrDefault());
            //await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/ListFlights");
        }

        public async Task<IActionResult> DeleteFlights(int id)
        {
            _context.flights.Remove(_context.flights.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/ListFlights");
        }

        [HttpPost]
        public async Task<IActionResult> ChooseRoute(int route)
        {
            //List<Flight> flights = _context.flights.Where(a => a.RouteId == route).ToList();
            //ViewData["flights"] = flights;

            //ViewData["routeChoose"] = routeObj;

            return RedirectToAction("ListFlights", "Dispatcher", new { id = route });
        }

        public IActionResult Trains()
        {
            List<Train> trains= _context.trains.ToList();
            ViewData["trains"] = trains;
            return View();
        }

        public IActionResult AddTrain(int id)
        {
            // получение списка станций
            List <RailwayCarriage> railwayCarriages= _context.railwayCarriages.Where(a => a.TrainId == id).ToList();
            ViewData["railwayCarriages"] = railwayCarriages;

            //List<RailwayCarriage> railwayCarriages = _context.railwayCarriages.Where(a => a.TrainId == id).ToList();
            //listStations.ForEach(s =>
            //{
            //    s.station = stations.Where(a=>a.Id==s.station);
            //});
            //ViewData["listStations"] = listStations;
            ViewData["trainId"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarriageToTrain(string idTrain, string CountSeats, string Num)
        {
            RailwayCarriage railwayCarriage = new RailwayCarriage();

            railwayCarriage.TrainId = Convert.ToInt32(idTrain);

            railwayCarriage.Num = Convert.ToInt32(Num);


            railwayCarriage.CountSeats= Convert.ToInt32(CountSeats);          
            _context.railwayCarriages.Add(railwayCarriage);
            await _context.SaveChangesAsync();
            return RedirectToAction("AddTrain", "Dispatcher", new { id = idTrain });
        }

        [HttpPost]
        public async Task<IActionResult> AddTrain(string Name)
        {
            Train train = new Train();

            train.Name = Name;
            _context.trains.Add(train);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Trains");
        }

        [HttpPost]
        public async Task<IActionResult> EditTrain(int id, string Name)
        {
            Train train = new Train();
            train.Name = Name;
            train.Id = id;

            _context.trains.Update(train);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Trains");
        }

        public async Task<IActionResult> DeleteTrain(int id)
        {
            //  удаление связанных списков станций


            _context.trains.Remove(_context.trains.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Trains");
        }

        public async Task<IActionResult> DeleteCarriageInTrain(int id, int IdRoute)
        {
            _context.railwayCarriages.Remove(_context.railwayCarriages.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return RedirectToAction("AddTrain", "Dispatcher", new { id = IdRoute });
        }

        public IActionResult PersonalMenu()
        { //список созданных им курсов

            return View();
        }

        public IActionResult Conductors()
        {
            List<Conductor> conductors = _context.conductors.ToList();
            ViewData["conductors"] = conductors;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddConductor(string Name,string Surname,string Fathername,string Email,string Password)
        {
            Conductor conductor= new Conductor();
            conductor.Name = Name;
            conductor.Surname = Surname;
            conductor.Fathername = Fathername;
            conductor.Email = Email;
            conductor.Password = Password;
            _context.conductors.Add(conductor);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Conductors");
        }

        [HttpPost]
        public async Task<IActionResult> EditConductor(int id, string Name, string Surname, string Fathername, string Email, string Password)
        {
            Conductor conductor= new Conductor();
            conductor.Name = Name;
            conductor.Id = id;
            conductor.Surname = Surname;
            conductor.Fathername = Fathername;
            conductor.Email = Email;
            conductor.Password = Password;

            _context.conductors.Update(conductor);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Conductors");
        }

        public async Task<IActionResult> DeleteConductor(int id)
        {
            _context.conductors.Remove(_context.conductors.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Conductors");
        }

        public IActionResult Machinists()
        {
            List<Machinist> machinists= _context.machinists.ToList();
            ViewData["machinists"] = machinists;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMachinist(string Name, string Surname, string Fathername, string Email, string Password)
        {
            Machinist machinist= new Machinist();
            machinist.Name = Name;
            machinist.Surname = Surname;
            machinist.Fathername = Fathername;
            machinist.Email = Email;
            machinist.Password = Password;
            _context.machinists.Add(machinist);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Machinists");
        }

        [HttpPost]
        public async Task<IActionResult> EditMachinist(int id, string Name, string Surname, string Fathername, string Email, string Password)
        {
            Machinist machinist= new Machinist();
            machinist.Name = Name;
            machinist.Id = id;
            machinist.Surname = Surname;
            machinist.Fathername = Fathername;
            machinist.Email = Email;
            machinist.Password = Password;

            _context.machinists.Update(machinist);
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Machinists");
        }

        public async Task<IActionResult> DeleteMachinist(int id)
        {
            _context.machinists.Remove(_context.machinists.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Machinists");
        }

        public IActionResult Trips(DateTime timeStart)
        {
            if (timeStart == null)
            {
                DateTime now = DateTime.Now;
                List<Trip> trips = _context.trips.Where(a => a.DateTimeLeave.Date == now.Date).ToList();
                ViewData["trips"] = trips;
            }
            else
            {
                List<Trip> trips = _context.trips.Where(a => a.DateTimeLeave.Date == timeStart.Date).ToList();
                ViewData["trips"] = trips;
            }
            List<Models.Route> routes= _context.routes.ToList();
            ViewData["routes"] = routes;

            List<Flight> flights = _context.flights.ToList();
            ViewData["flights"] = flights;

            List<Train> trains= _context.trains.ToList();
            ViewData["trains"] = trains;

            List<RailwayCarriage> railwayCarriages= _context.railwayCarriages.ToList();
            ViewData["railwayCarriages"] = railwayCarriages;

            List<Ticket> tickets= _context.tickets.ToList();
            ViewData["tickets"] = tickets;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChooseDateTrip(DateTime TimeStart)
        {
            //List<Flight> flights = _context.flights.Where(a => a.RouteId == route).ToList();
            //ViewData["flights"] = flights;

            //ViewData["routeChoose"] = routeObj;

            return RedirectToAction("Trips", "Dispatcher", new { timeStart = TimeStart });
        }

        public async Task<IActionResult> DeleteTrip(int id)
        {
            _context.trips.Remove(_context.trips.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return Redirect("~/Dispatcher/Trips");
        }
    }
}