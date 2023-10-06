using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Billing
{

    public class OrderPlacedHandler :
        IHandleMessages<OrderPlaced>
    {
        static readonly ILog log = LogManager.GetLogger<OrderPlacedHandler>();

        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Billing has received OrderPlaced, OrderId = {message.OrderId}");

            await context.Publish( new OrderBilled {  OrderId = message.OrderId } );
        }
    }
}