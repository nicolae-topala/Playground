using NServiceBus.Logging;
using SmartHome.Common.Messages;

namespace SmartHome.CoffeeMachine.Endpoint
{
    public class CoffeeMadeHandler : IHandleMessages<CoffeeMade>
    {
        static ILog log = LogManager.GetLogger<MakeCoffeeHandler>();

        public Task Handle(CoffeeMade message, IMessageHandlerContext context)
        {
            log.Info($"{message.CoffeeId} is ready to be served");
            return Task.CompletedTask;
        }
    }
}
