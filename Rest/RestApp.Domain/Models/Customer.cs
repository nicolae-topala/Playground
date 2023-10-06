namespace RestApp.Domain.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public string? Address { get; set; }
    public int TotalProducts { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
