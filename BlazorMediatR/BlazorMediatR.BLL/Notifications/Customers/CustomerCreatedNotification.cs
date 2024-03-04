using BlazorMediatR.Common.DTOs.Customer;
using MediatR;

namespace BlazorMediatR.BLL.Notifications.Customers
{
    public record CustomerCreatedNotification(CustomerDto Customer) : INotification;
}
