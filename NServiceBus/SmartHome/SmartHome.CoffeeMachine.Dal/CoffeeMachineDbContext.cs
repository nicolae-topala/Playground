using Microsoft.EntityFrameworkCore;
using SmartHome.Common.Models;
using CoffeeMachineModel = SmartHome.Common.Models.CoffeeMachine;

namespace SmartHome.CoffeeMachine.Dal
{
    public class CoffeeMachineDbContext : DbContext
    {
        public CoffeeMachineDbContext(DbContextOptions<CoffeeMachineDbContext> options) : base(options) { }

        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<CoffeeMachineModel> CoffeeMachines { get; set; }
    }
}
