namespace WebApp.Common.DTOs.Product
{
    public record CreateProductDto
    {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
    }
}
