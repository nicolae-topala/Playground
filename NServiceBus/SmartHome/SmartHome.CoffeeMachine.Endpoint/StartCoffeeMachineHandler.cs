using Microsoft.EntityFrameworkCore;
using NServiceBus.Logging;
using SmartHome.CoffeeMachine.Dal;
using SmartHome.Common.Messages;

namespace SmartHome.CoffeeMachine.Endpoint
{
    public class StartCoffeeMachineHandler : IHandleMessages<StartCoffeeMachine>
    {
        private readonly CoffeeMachineDbContext _context;
        static ILog log = LogManager.GetLogger<MakeCoffeeHandler>();

        public StartCoffeeMachineHandler(CoffeeMachineDbContext context)
        {
            _context = context;
        }

        public async Task Handle(StartCoffeeMachine message, IMessageHandlerContext context)
        {
            var coffeeMachine = await _context.CoffeeMachines
                .Where(x => x.IsActive == false)
                .FirstOrDefaultAsync();

            if (coffeeMachine == null)
            {
                log.Info($"No Coffee Machines available.");
                return;
            }

            coffeeMachine.IsActive = true;
            await _context.SaveChangesAsync();

            log.Info($"Coffee Machine with ID: {coffeeMachine.CoffeeMachineId}, started.");

            var newMessage = new MakeCoffee
            {
                CoffeeId = message.CoffeeId,
                CoffeeMachineId = coffeeMachine.CoffeeMachineId,
            };
            await context.Send(newMessage);
        }
    }
}
