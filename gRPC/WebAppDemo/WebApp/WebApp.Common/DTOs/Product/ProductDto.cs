namespace WebApp.Common.DTOs.Product
{
    public record ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
    }
}
