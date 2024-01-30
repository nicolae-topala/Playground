using GraphQL.Common.Dtos;
using GraphQL.DAL;
using GraphQL.Domain.Models;
using HotChocolate;
using HotChocolate.Subscriptions;
using Mapster;

namespace GraphQL.BLL.Mutations
{
    public class Mutation
    {
        public async Task<User> AddUserAsync(SqlDbContext context, CreateUserDto userDto, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
        {
            var user = userDto.Adapt<User>();
            user.Id = Guid.NewGuid();

            await context.Users.AddAsync(user, cancellationToken); 
            await context.SaveChangesAsync(cancellationToken);

            await sender.SendAsync(nameof(AddUserAsync), user, cancellationToken);

            return user;
        }

        public async Task<Product> AddProductForUserAsync(SqlDbContext context, AddProductDto productDto, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
        {
            var product = productDto.Adapt<Product>();
            product.Id = Guid.NewGuid();

            await context.Products.AddAsync(product, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            await sender.SendAsync(product.UserId.ToString(), product, cancellationToken);

            return product;
        }
    }
}
