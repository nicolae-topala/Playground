using Microsoft.EntityFrameworkCore;
using RestApp.Common.Dtos;
using RestApp.Dal.Interfaces;
using RestApp.DemoMigrations;
using RestApp.Domain.Models;
using System.Runtime.CompilerServices;

namespace RestApp.Dal.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DemoDatabaseContext _context;

        public CustomerRepository(DemoDatabaseContext context) {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomersProcedureAsync()
        {
            var procedureName = FormattableStringFactory.Create("SelectAllCustomers");
            var customers = await _context.Customers.FromSql(procedureName).ToListAsync();

            return customers;
        }
        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.Products)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    Address = c.Address,
                    TotalProducts = c.TotalProducts,
                    ProductsId = c.Products.Select(p => p.ProductId).ToList(),
                })
                .ToListAsync();

            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _context.Customers.FromSql($"SelectCustomer '{customerId}'").FirstAsync();

            return customer;
        }
    }
}
