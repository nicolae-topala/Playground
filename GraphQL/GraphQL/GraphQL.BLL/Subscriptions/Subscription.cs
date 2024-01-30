using GraphQL.BLL.Mutations;
using GraphQL.Domain.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL.BLL.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        [Topic(nameof(Mutation.AddUserAsync))]
        public User UserAdded([EventMessage] User user) => 
            user;

        [Subscribe]
        [Topic("{userId}")]
        public Product ProductAdded(string userId, [EventMessage] Product product) =>
            product;
    }
}
