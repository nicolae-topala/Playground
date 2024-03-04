namespace BlazorMediatR.Common.DTOs.Customer
{
    public record CreateCustomerDto
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
    }
}
