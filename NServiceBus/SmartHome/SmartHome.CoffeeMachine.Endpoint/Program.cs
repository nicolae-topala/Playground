using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.CoffeeMachine.Dal;
using SmartHome.Common.Messages;

Console.Title = "SmartHome.CoffeeMachine.Endpoint";
var connectionString = "Server=127.0.0.1,1633;Database=coffeeMachinesDb;User Id=sa;Password=Qwerty123$;TrustServerCertificate=True;";

var endpointConfiguration = new EndpointConfiguration("SmartHome.CoffeeMachine.Endpoint");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();

var transport = endpointConfiguration.UseTransport<LearningTransport>();

var routing = transport.Routing();
routing.RouteToEndpoint(typeof(MakeCoffee), "SmartHome.CoffeeMachine.Endpoint");

endpointConfiguration.RegisterComponents(c =>
{
    var dbContextOptions = new DbContextOptionsBuilder<CoffeeMachineDbContext>()
                .UseSqlServer(connectionString)
                .Options;

    var dbContext = new CoffeeMachineDbContext(dbContextOptions);
    c.AddSingleton(dbContext);
});

var endpointInstance = await Endpoint.Start(endpointConfiguration);
Console.WriteLine("Press any key to exit");
Console.ReadKey();
await endpointInstance.Stop();