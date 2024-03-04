using WebApp.Common.DTOs.Product;

namespace WebApp.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> AddNewProductAsync(CreateProductDto productDto);
        Task<ProductDto?> GetProductByIdAsync(Guid productId);
    }
}

