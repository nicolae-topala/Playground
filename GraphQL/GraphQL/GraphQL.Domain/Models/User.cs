using Mapster;

namespace GraphQL.Domain.Models
{
    [AdaptTo("RegisterDto"), GenerateMapper]
    public class User
    {
        [AdaptIgnore]
        public Guid Id { get; set; }
        [AdaptMember(Name = "Username")]
        public string Userame { get; set; } = null!;
        public string Email { get; set; } = null!;

        [GraphQLIgnore]
        public string Password { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = null!;
    }
}
