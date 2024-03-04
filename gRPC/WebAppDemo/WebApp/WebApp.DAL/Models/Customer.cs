namespace WebApp.DAL.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
