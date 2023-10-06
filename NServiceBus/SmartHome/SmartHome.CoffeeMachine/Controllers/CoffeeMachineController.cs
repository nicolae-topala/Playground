using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHome.CoffeeMachine.Dal;
using SmartHome.Common.Messages;

namespace SmartHome.CoffeeMachine.Controllers
{
    [Route("api/coffeeMachine")]
    public class CoffeeMachineController : ControllerBase
    {
        private readonly CoffeeMachineDbContext _context;
        private readonly IMessageSession _messageSession;

        public CoffeeMachineController(CoffeeMachineDbContext context, IMessageSession messageSession)
        {
            _context = context;
            _messageSession = messageSession;
        }

        [HttpGet("CoffeeMachines")]
        public async Task<IActionResult> GetAllCofeeMachines()
        {
            var result = await _context.CoffeeMachines
                .Select(x => new
                {
                    x.CoffeeMachineId,
                    x.CoffeeMachineSerialNr,
                    x.IsActive,
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("Coffees")]
        public async Task<IActionResult> GetCoffees()
        {
            var coffees = await _context.Coffees
                .Select(x => new
                {
                    x.CoffeeId,
                    x.CoffeeMachineId,
                    x.IsDone,
                })
                .ToListAsync();
            return Ok(coffees);
        }

        [HttpPost]
        public async Task<IActionResult> MakeCoffee()
        {
            var command = new StartCoffeeMachine
            {
                CoffeeId = Guid.NewGuid()
            };
            await _messageSession.Send(command);

            return Ok($"Coffee {command.CoffeeId} is in progress.");
        }
    }
}
