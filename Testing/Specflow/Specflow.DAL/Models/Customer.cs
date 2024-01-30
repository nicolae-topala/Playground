namespace xUnit.DAL.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public int TotalProducts { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
