using Grpc.Core;
using WebApp.Common.Protos;

namespace Client.API.Services
{
    public class OrderService : ExternalOrder.ExternalOrderBase
    {
        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;
        }
        public override async Task<ProcessedOrder> ProcessOrderById(ProcessOrder request, ServerCallContext context)
        {
            _logger.LogInformation($"Accepted request for order:{request.OrderId}");
            // Imitate a a stagin processing scenario
            await Task.Delay(10000);

            _logger.LogInformation($"Processing started for order:${request.OrderId}");

            return new ProcessedOrder()
            {
                Status = OrderStatus.Processing
            };
        }
    }
}
