using BlazorMediatR.Common.DTOs.Customer;
using BlazorMediatR.DAL.Interfaces;
using BlazorMediatR.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMediatR.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto?> CreateNewCustomerAsync(CreateCustomerDto customerDto)
        {
            Customer customer = new()
            {
                Id = Guid.NewGuid(),
                Name = customerDto.Name,
                Address = customerDto.Address
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            CustomerDto response = new()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address
            };

            return response;
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync() => await _context.Customers
            .AsNoTracking()
            .Select(x => new CustomerDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToListAsync();

        public async Task<CustomerDto?> GetCustomerByNameAsync(string customerName) => await _context.Customers
            .AsNoTracking()
            .Select(x => new CustomerDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            })
            .FirstOrDefaultAsync(x => x.Name == customerName);
    }
}
