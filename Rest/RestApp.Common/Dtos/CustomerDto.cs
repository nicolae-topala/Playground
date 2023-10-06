namespace RestApp.Common.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? Address { get; set; }
        public int TotalProducts { get; set; }
        public List<int> ProductsId { get; set; }
    }
}
