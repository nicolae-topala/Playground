using WebApp.BLL.Interfaces;
using WebApp.Common.DTOs.Product;
using WebApp.DAL.Interfaces;

namespace WebApp.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> AddProductAsync(CreateProductDto productDto) =>
            await _productRepository.AddNewProductAsync(productDto);

        public async Task<List<ProductDto>> GetAllCustomersAsync() =>
            await _productRepository.GetAllProductsAsync();
    }
}
