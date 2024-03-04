namespace WebApp.Common.DTOs.Order
{
    public record CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public List<Guid> ProductsId { get; set; } = [];
    }
}
