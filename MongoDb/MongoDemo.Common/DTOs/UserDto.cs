namespace MongoDemo.Common.DTOs
{
    public record UserDto
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public DateOnly DateOfBirth { get; init; }
    }
}   
