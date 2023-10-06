using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestApp.Domain.Models;

namespace RestApp.DemoMigrations;


public partial class DemoDatabaseContext : DbContext
{

    public DemoDatabaseContext(DbContextOptions<DemoDatabaseContext> options) : base(options) {}

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable(tb =>
                {
                    tb.HasTrigger("DecrementTotalProducts");
                    tb.HasTrigger("IncrementTotalProducts");
                });

            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Products)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Customers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
