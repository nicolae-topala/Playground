using Microsoft.EntityFrameworkCore;
using WebApp.DAL.Models;

namespace WebApp.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {  
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(x => x.Order)
                .WithMany(y => y.OrderProducts)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(x => x.Product)
                .WithMany(y => y.OrderProducts)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
