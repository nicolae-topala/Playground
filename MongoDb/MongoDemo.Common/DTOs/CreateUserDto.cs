namespace MongoDemo.Common.DTOs
{
    public record CreateUserDto
    {
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public DateOnly DateOfBirth { get; init; }
    }
}
