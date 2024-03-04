using WebApp.BLL.Interfaces;
using WebApp.Common.DTOs.Customer;
using WebApp.DAL.Interfaces;

namespace WebApp.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto?> CreateNewCustomerAsync(CreateCustomerDto customerDto) => 
            await _customerRepository.CreateNewCustomerAsync(customerDto);


        public async Task<List<CustomerDto>> GetAllCustomersAsync() =>
            await _customerRepository.GetAllCustomersAsync();

        public async Task<CustomerDto?> GetCustomerByNameAsync(string customerName) =>
            await _customerRepository.GetCustomerByNameAsync(customerName);
    }
}
