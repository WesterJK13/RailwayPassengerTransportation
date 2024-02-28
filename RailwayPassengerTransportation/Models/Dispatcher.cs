using Microsoft.AspNetCore.Identity;

namespace RailwayPassengerTransportation.Models
{
    public class Dispatcher
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Surname { get; set; }
        public string? Fathername { get; set; }
        public string? Name { get; set; }
    }
}
