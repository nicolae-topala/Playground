using WebApp.Common.DTOs.Product;

namespace WebApp.BLL.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllCustomersAsync();
        Task<ProductDto?> AddProductAsync(CreateProductDto productDto);
    }
}
