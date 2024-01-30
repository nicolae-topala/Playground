namespace GraphQL.Common.Dtos
{
    public record CreateUserDto
    {
        public string Username { get; init; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
