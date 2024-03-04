using BlazorMediatR.Common.DTOs.Customer;
using MediatR;

namespace BlazorMediatR.BLL.Commands
{
    public record CreateNewCustomerCommand(CreateCustomerDto CustomerDto) : IRequest<CustomerDto>;
}
