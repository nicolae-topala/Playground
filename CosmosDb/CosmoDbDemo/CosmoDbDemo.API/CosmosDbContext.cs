using CosmoDbDemo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmoDbDemo.API
{
    public class CosmosDbContext : DbContext
    {
        public DbSet<Employee>? Employees { get; set; }

        public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                    .ToContainer("Employees") 
                    .HasPartitionKey(e => e.Id);
        }
    }
}
