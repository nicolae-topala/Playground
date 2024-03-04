using BlazorMediatR.BLL.Notifications.Customers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlazorMediatR.BLL.Handlers.Customers
{
    public class CustomerCreatedHandler : INotificationHandler<CustomerCreatedNotification>
    {
        private readonly ILogger<CustomerCreatedHandler> _logger;

        public CustomerCreatedHandler(ILogger<CustomerCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerCreatedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"New customer created: ${notification.Customer}");
            return Task.CompletedTask;
        }
    }
}
