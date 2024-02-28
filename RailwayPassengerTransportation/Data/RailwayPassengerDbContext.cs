using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RailwayPassengerTransportation.Models;
using static System.Net.Mime.MediaTypeNames;

namespace RailwayPassengerTransportation.Data
{
    public class RailwayPassengerDbContext : DbContext
    {
        public RailwayPassengerDbContext(DbContextOptions<RailwayPassengerDbContext> options) : base(options) { }

        //поля из таблиц
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Conductor> conductors { get; set; }
        public DbSet<Admin> admins{ get; set; }
        public DbSet<Dispatcher> dispatchers { get; set; }
        public DbSet<Flight> flights { get; set; }
        public DbSet<ListDayWeeks> listDayWeeks { get; set; }
        public DbSet<ListStations> listStations { get; set; }
        public DbSet<Machinist> machinists { get; set; }
        public DbSet<Passenger> passengers { get; set; }
        public DbSet<RailwayCarriage> railwayCarriages { get; set; }
        public DbSet<Models.Route> routes { get; set; }
        public DbSet<Station> stations { get; set; }
        public DbSet<Train> trains { get; set; }
        //public DbSet<TrainComposition> trainCompositions { get; set; }
        public DbSet<Trip> trips { get; set; }
        public DbSet<Ticket> tickets { get; set; }
    }
}
