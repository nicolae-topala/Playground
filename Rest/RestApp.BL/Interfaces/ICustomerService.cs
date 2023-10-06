using RestApp.Common.Dtos;
using RestApp.Domain.Models;

namespace RestApp.BL.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<List<CustomerDto>> GetAllCustomersRedisAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);
    }
}
