using GraphQL.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.DAL
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
