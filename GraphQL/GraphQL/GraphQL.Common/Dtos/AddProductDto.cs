namespace GraphQL.Common.Dtos
{
    public record AddProductDto
    {
        public string Name { get; init; } = null!;
        public decimal Price { get; init; }
        public string Description { get; init; } = null!;
        public Guid UserId { get; init; }
    }
}
