namespace RestApp.Domain.Models;

public partial class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Price { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
}
