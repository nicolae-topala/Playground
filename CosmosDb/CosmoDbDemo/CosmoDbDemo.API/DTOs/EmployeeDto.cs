namespace CosmoDbDemo.API.DTOs
{
    public record EmployeeDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Address { get; init; }
    }
}
