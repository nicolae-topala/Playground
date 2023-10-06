using Microsoft.EntityFrameworkCore;
using SmartHome.Common.Models;

namespace SmartHome.Lights.Dal
{
    public class LightsDbContext : DbContext
    {
        public LightsDbContext(DbContextOptions<LightsDbContext> options) : base(options) { }

        public DbSet<Light> Lights { get; set; }
        public DbSet<Position> Positions { get; set; }
    }

}
