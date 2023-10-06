using Messages;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    public class ShippingPolicy : Saga<ShippingPolicy.SagaData>,
        IAmStartedByMessages<OrderPlaced>,
        IAmStartedByMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<ShippingPolicy>();

        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            this.Data.Placed = true;
            log.Info($"Received OrderPlaced, OrderId = {message.OrderId}, status (Placed = {Data.Placed}, Billed={Data.Billed})");

            await ProcessOrder(context);
        }

        public async Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            this.Data.Billed = true;
            log.Info($"Received OrderBilled, OrderId = {message.OrderId}, status (Placed = {Data.Placed}, Billed={Data.Billed})");

            await ProcessOrder(context);
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.Placed && Data.Billed)
            {
                log.Info($"Order is placed & billed, time to ship!");
                MarkAsComplete();
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.OrderId)
                .ToMessage<OrderPlaced>(msg => msg.OrderId)
                .ToMessage<OrderBilled>(msg => msg.OrderId);
        }

        public class SagaData : ContainSagaData
        {
            public string OrderId { get; set; }
            public bool Placed { get; set; }
            public bool Billed { get; set; }
        }
    }
}
