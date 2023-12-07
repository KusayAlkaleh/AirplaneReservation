using Microsoft.EntityFrameworkCore;
using WebProject.Models.Domain;

namespace WebProject.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
