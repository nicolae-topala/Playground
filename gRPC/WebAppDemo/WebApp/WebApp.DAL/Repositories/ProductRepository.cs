using Microsoft.EntityFrameworkCore;
using WebApp.Common.DTOs.Customer;
using WebApp.Common.DTOs.Product;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Models;

namespace WebApp.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto?> AddNewProductAsync(CreateProductDto productDto)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            ProductDto response = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return response;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync() =>
            await _context.Products.AsNoTracking()
                .Select(x => new ProductDto 
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToListAsync();

        public async Task<ProductDto?> GetProductByIdAsync(Guid productId) =>
            await _context.Products.AsNoTracking()
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                })
                .FirstOrDefaultAsync(x => x.Id == productId);
    }
}
