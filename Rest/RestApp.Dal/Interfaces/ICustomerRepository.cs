using RestApp.Common.Dtos;
using RestApp.Domain.Models;

namespace RestApp.Dal.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetCustomersAsync();
        Task<List<Customer>> GetCustomersProcedureAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);
    }
}
