using BlazorMediatR.BLL.Queries.Customers;
using BlazorMediatR.Common.DTOs.Customer;
using BlazorMediatR.DAL.Interfaces;
using MediatR;

namespace BlazorMediatR.BLL.Handlers.Customers
{
    public class GetCustomerByNameHandler : IRequestHandler<GetCustomerByNameQuery, CustomerDto?>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerByNameHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerDto?> Handle(GetCustomerByNameQuery request, CancellationToken cancellationToken) =>
            await _repository.GetCustomerByNameAsync(request.CustomerName);
    }
}
