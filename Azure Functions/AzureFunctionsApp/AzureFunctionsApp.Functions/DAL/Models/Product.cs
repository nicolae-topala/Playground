namespace AzureFunctionsApp.DAL.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;
    }
}
