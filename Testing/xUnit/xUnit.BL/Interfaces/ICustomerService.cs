using xUnit.Common.Dtos;

namespace xUnit.BL.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(Guid customerId);
        Task<Guid> CreateCustomerAsync(CreateCustomerDto model);
    }
}
