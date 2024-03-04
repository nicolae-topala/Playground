namespace WebApp.Common.DTOs.Customer
{
    public record CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
    }
}
