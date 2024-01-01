using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProject.Models.Domain;

namespace WebProject.Data
{
    public class DemoDbContext : IdentityDbContext<ApplicationUser>
    {
        public DemoDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
    }
}
