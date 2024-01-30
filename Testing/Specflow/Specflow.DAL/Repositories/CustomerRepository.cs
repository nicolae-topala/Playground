using Microsoft.EntityFrameworkCore;
using xUnit.Common.Dtos;
using xUnit.DAL.Models;
using xUnit.DAL.Interfaces;

namespace xUnit.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.Products)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.Id,
                    CustomerName = c.Name,
                    Address = c.Address,
                    TotalProducts = c.TotalProducts,
                    ProductsId = c.Products.Select(p => p.Id).ToList(),
                })
                .ToListAsync();

            return customers;
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(Guid customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Products)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.Id,
                    CustomerName = c.Name,
                    Address = c.Address,
                    TotalProducts = c.TotalProducts,
                    ProductsId = c.Products.Select(p => p.Id).ToList(),
                })
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            return customer;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<CustomerDto?> GetCustomerByNameAsync(string customerName)
        {
            var customer = await _context.Customers
                .Include(c => c.Products)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.Id,
                    CustomerName = c.Name,
                    Address = c.Address,
                    TotalProducts = c.TotalProducts,
                    ProductsId = c.Products.Select(p => p.Id).ToList(),
                })
                .FirstOrDefaultAsync(c => c.CustomerName == customerName);

            return customer;
        }
    }
}
