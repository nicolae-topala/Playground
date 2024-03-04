using WebApp.Common.DTOs.Order;
using WebApp.Common.Protos;

namespace WebApp.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> PostNewOrderAsync(CreateOrderDto orderDto);
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
        Task UpdateOrderStatusAsync(Guid id, OrderStatus status);
    }
}
