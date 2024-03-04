using WebApp.Common.Protos;

namespace WebApp.Common.DTOs.Order
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public Guid CustomerId { get; set; }
        public List<Guid> ProductsId { get; set; } = [];
    }
}
