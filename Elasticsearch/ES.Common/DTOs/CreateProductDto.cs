namespace ES.Common.DTOs
{
    public record UpdateProductDto
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public decimal Price { get; init; }
    }
}
