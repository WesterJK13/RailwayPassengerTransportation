using Microsoft.AspNetCore.Identity;

namespace RailwayPassengerTransportation.Models
{
    public class Passenger
    {
        // обычный пользователь
        public int Id { get; set; }
        // привязка к user
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Passport { get; set; }
        public string? Surname { get; set; }
        public string? Fathername { get; set; }
        public string? Name { get; set; }
        // кол-во средств на счете
        public double? Money { get; set; }
    }
}
