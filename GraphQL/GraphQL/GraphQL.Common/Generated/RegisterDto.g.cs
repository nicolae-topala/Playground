using System.Collections.Generic;
using GraphQL.Domain.Models;

namespace GraphQL.Common.Generated
{
    public partial class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}