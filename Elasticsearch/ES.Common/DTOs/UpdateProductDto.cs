namespace ES.Common.DTOs
{
    public record CreateProductDto
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public decimal Price { get; init; }
    }
}
