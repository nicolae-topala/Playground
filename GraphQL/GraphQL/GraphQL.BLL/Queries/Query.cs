using GraphQL.DAL;
using GraphQL.Domain.Models;
using HotChocolate.Types;

namespace GraphQL.BLL.Queries
{
    public class Query
    {
        [UsePaging(MaxPageSize = 3, IncludeTotalCount = true)]
        [UseSorting]
        [UseFiltering]
        public IQueryable<User> GetUsers(SqlDbContext context) =>
            context.Users;
    }
}
