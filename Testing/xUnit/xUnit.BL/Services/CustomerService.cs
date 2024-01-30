using Microsoft.Extensions.Logging;
using xUnit.BL.Interfaces;
using xUnit.Common.Dtos;
using xUnit.DAL.Models;
using xUnit.DAL.Interfaces;

namespace xUnit.BL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Guid> CreateCustomerAsync(CreateCustomerDto model)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = model.CustomerName,
                Address = model.Address,
            };

            await _customerRepository.CreateCustomerAsync(customer);
            return customer.Id;
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync() =>
            await _customerRepository.GetCustomersAsync();

        public async Task<CustomerDto?> GetCustomerByIdAsync(Guid customerId)
        {
            _logger.LogInformation($"Customer with ID: {customerId} was accessed");
            var result = await _customerRepository.GetCustomerByIdAsync(customerId);

            return result;
        }
    }
}
