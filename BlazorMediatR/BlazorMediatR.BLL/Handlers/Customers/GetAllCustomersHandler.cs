using BlazorMediatR.BLL.Queries.Customers;
using BlazorMediatR.Common.DTOs.Customer;
using BlazorMediatR.DAL.Interfaces;
using MediatR;

namespace BlazorMediatR.BLL.Handlers.Customers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersHandler(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request,
            CancellationToken cancellationToken) => await _customerRepository.GetAllCustomersAsync();
    }
}
