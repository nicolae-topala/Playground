using AzureFunctionsApp.Common.DTOs.Customer;

namespace AzureFunctionsApp.DAL.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByNameAsync(string customerName);
        Task<CustomerDto?> CreateNewCustomerAsync(CreateCustomerDto customerDto);
    }
}
