namespace xUnit.Common.Dtos
{
    public class CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? Address { get; set; }
        public int TotalProducts { get; set; }
        public List<Guid>? ProductsId { get; set; }
    }
}
