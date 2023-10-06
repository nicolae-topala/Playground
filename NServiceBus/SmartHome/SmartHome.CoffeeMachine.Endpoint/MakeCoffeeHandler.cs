using NServiceBus.Logging;
using SmartHome.CoffeeMachine.Dal;
using SmartHome.Common.Messages;
using SmartHome.Common.Models;

namespace SmartHome.CoffeeMachine.Endpoint
{
    public class MakeCoffeeHandler : IHandleMessages<MakeCoffee>
    {
        static ILog log = LogManager.GetLogger<MakeCoffeeHandler>();

        private readonly CoffeeMachineDbContext _context;

        public MakeCoffeeHandler(CoffeeMachineDbContext context)
        {
            _context = context;
        }

        public async Task Handle(MakeCoffee message, IMessageHandlerContext context)
        {
            log.Info($"Coffee {message.CoffeeId} is in progress.");

            var coffee = new Coffee
            {
                CoffeeId = message.CoffeeId,
                CoffeeMachineId = message.CoffeeMachineId,
                IsDone = false,
            };
            var coffeeMachine = await _context.CoffeeMachines
                .FindAsync(message.CoffeeMachineId);

            await _context.Coffees.AddAsync(coffee);
            await _context.SaveChangesAsync();

            // Faking a waiting proccess from the actual machine
            await Task.Delay(5000);

            coffee.IsDone = true;
            coffeeMachine.IsActive = false;
            await _context.SaveChangesAsync();

            var newMessage = new CoffeeMade
            {
                CoffeeId = coffee.CoffeeId,
            };
            await context.Publish(newMessage);
        }
    }

}
