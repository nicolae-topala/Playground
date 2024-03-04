using WebApp.Common.DTOs.Order;

namespace WebApp.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetOrdersAsync();
        Task<OrderDto?> PostOrderAsync(CreateOrderDto orderDto);
    }
}
