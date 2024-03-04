using WebApp.Common.Protos;

namespace WebApp.DAL.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = [];
    }
}
