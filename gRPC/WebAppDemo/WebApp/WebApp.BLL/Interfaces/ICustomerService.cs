using WebApp.Common.DTOs.Customer;

namespace WebApp.BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByNameAsync(string customerName);
        Task<CustomerDto?> CreateNewCustomerAsync(CreateCustomerDto customerDto);
    }
}
