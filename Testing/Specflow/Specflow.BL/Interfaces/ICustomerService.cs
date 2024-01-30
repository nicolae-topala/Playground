using xUnit.Common.Dtos;

namespace xUnit.BL.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(Guid customerId);
        Task<CustomerDto?> GetCustomerByNameAsync(string customerName);
        Task<Guid> CreateCustomerAsync(CreateCustomerDto model);
    }
}
