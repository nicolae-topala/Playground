using BlazorMediatR.Common.DTOs.Customer;
using MediatR;

namespace BlazorMediatR.BLL.Queries.Customers
{
    public record GetCustomerByNameQuery(string CustomerName) : IRequest<CustomerDto?>;
}
