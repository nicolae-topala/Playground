using Microsoft.EntityFrameworkCore;
using RabbitMQ.DAL.Models;

namespace RabbitMQ.DAL
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Student { get; set; }
    }
}
