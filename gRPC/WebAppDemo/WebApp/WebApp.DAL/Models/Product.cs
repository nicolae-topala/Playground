namespace WebApp.DAL.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = [];
    }
}
