using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp.BLL.Interfaces;
using WebApp.Common.DTOs.Order;
using WebApp.Common.Protos;
using WebApp.DAL.Interfaces;

namespace WebApp.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ExternalOrder.ExternalOrderClient _client;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OrderService(IOrderRepository orderRepository, ExternalOrder.ExternalOrderClient client, IServiceScopeFactory serviceScopeFactory)
        {
            _orderRepository = orderRepository;
            _client = client;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<List<OrderDto>> GetOrdersAsync() =>
            await _orderRepository.GetAllOrdersAsync();

        public async Task<OrderDto?> PostOrderAsync(CreateOrderDto orderDto)
        {
            var result = await _orderRepository.PostNewOrderAsync(orderDto);

            if (result is null) return null;

            ProcessOrder processOrder = new()
            {
                OrderId = result.Id.ToString(),
            };

            Task.Run(() => ProcessOrderInBackground(result.Id));

            return result;
        }


        private async Task ProcessOrderInBackground(Guid orderId)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var client = scope.ServiceProvider.GetRequiredService<ExternalOrder.ExternalOrderClient>();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<OrderService>>();

            ProcessOrder processOrder = new()
            {
                OrderId = orderId.ToString(),
            };

            try
            {
                var result = await client.ProcessOrderByIdAsync(processOrder);
                logger.LogInformation($"Order status for ID:{orderId} is being changed to {result.Status}");
                await orderRepository.UpdateOrderStatusAsync(orderId, result.Status);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
