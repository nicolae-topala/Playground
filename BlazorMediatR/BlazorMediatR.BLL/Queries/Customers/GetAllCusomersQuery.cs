using BlazorMediatR.Common.DTOs.Customer;
using MediatR;

namespace BlazorMediatR.BLL.Queries.Customers
{
    public record GetAllCustomersQuery() : IRequest<IEnumerable<CustomerDto>>;
}
