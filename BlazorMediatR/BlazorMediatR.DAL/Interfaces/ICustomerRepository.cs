using BlazorMediatR.Common.DTOs.Customer;

namespace BlazorMediatR.DAL.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByNameAsync(string customerName);
        Task<CustomerDto?> CreateNewCustomerAsync(CreateCustomerDto customerDto);
    }
}
