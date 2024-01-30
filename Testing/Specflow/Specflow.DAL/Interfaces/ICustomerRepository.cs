using xUnit.Common.Dtos;
using xUnit.DAL.Models;

namespace xUnit.DAL.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(Guid customerId);
        Task<CustomerDto?> GetCustomerByNameAsync(string customerName);
        Task CreateCustomerAsync(Customer customer);
    }
}
