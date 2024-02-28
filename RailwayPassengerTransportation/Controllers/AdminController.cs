using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RailwayPassengerTransportation.Data;
using RailwayPassengerTransportation.Models;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

namespace RailwayPassengerTransportation.Controllers
{
    public class AdminController : Controller
    {
        private readonly RailwayPassengerDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger, RailwayPassengerDbContext railwayDbContext)
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


        public IActionResult User()
        {
            List<Passenger> passengers= _context.passengers.ToList();
            ViewData["passengers"] = passengers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string Name, string Surname, string Fathername, string Email, string Password)
        {
            Passenger passenger= new Passenger();
            passenger.Name = Name;
            passenger.Surname = Surname;
            passenger.Fathername = Fathername;
            passenger.Email = Email;
            passenger.Password = Password;
            passenger.Money= 0;
            _context.passengers.Add(passenger);
            await _context.SaveChangesAsync();
            return Redirect("~/Admin/User");
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(int id, string Name, string Surname, string Fathername, string Email, string Password)
        {
            Passenger passenger= new Passenger();
            passenger.Id = id;
            passenger.Name = Name;
            passenger.Surname = Surname;
            passenger.Fathername = Fathername;
            passenger.Email = Email;
            passenger.Password = Password;
            passenger.Money = _context.passengers.Where(x => x.Id == passenger.Id).Select(x => x.Money).First();

            _context.passengers.Update(passenger);
            await _context.SaveChangesAsync();
            return Redirect("~/Admin/User");
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            _context.passengers.Remove(_context.passengers.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return Redirect("~/Admin/User");
        }


        public IActionResult Dispetcher()
        {
            List<Dispatcher> dispatchers= _context.dispatchers.ToList();
            ViewData["dispatchers"] = dispatchers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDispetcher(string Name, string Surname, string Fathername, string Email, string Password)
        {
            Dispatcher dispatcher= new Dispatcher();
            dispatcher.Name = Name;
            dispatcher.Surname = Surname;
            dispatcher.Fathername = Fathername;
            dispatcher.Email = Email;
            dispatcher.Password = Password;
            _context.dispatchers.Add(dispatcher);
            await _context.SaveChangesAsync();
            return Redirect("~/Admin/Dispetcher");
        }

        [HttpPost]
        public async Task<IActionResult> EditDispetcher(int id, string Name, string Surname, string Fathername, string Email, string Password)
        {
            Dispatcher dispatcher= new Dispatcher();
            dispatcher.Id = id;
            dispatcher.Name = Name;
            dispatcher.Surname = Surname;
            dispatcher.Fathername = Fathername;
            dispatcher.Email = Email;
            dispatcher.Password = Password;

            _context.dispatchers.Update(dispatcher);
            await _context.SaveChangesAsync();
            return Redirect("~/Admin/Dispetcher");
        }

        public async Task<IActionResult> DeleteDispetcher(int id)
        {
            _context.dispatchers.Remove(_context.dispatchers.Where(a => a.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
            return Redirect("~/Admin/Dispetcher");
        }
    }
}