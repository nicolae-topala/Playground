using BlazorMediatR.BLL.Commands;
using BlazorMediatR.BLL.Notifications.Customers;
using BlazorMediatR.Common.DTOs.Customer;
using BlazorMediatR.DAL.Interfaces;
using MediatR;

namespace BlazorMediatR.BLL.Handlers.Customers
{
    public class CreateNewCustomerHandler : IRequestHandler<CreateNewCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _repository;
        private readonly IPublisher _mediator;

        public CreateNewCustomerHandler(ICustomerRepository repository, IPublisher mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<CustomerDto> Handle(CreateNewCustomerCommand request, CancellationToken cancellationToken)
        {
            CustomerDto customer = await _repository.CreateNewCustomerAsync(request.CustomerDto);

            _mediator.Publish(new CustomerCreatedNotification(customer));
            return customer;
        }
    }
}
