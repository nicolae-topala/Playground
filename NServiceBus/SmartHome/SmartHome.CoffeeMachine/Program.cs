using Microsoft.EntityFrameworkCore;
using SmartHome.CoffeeMachine.Dal;
using SmartHome.Common.Messages;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Configure NServiceBus
builder.Host.UseNServiceBus(context =>
{
    var endpointConfiguration = new EndpointConfiguration("SmartHome.CoffeeMachine.Sender");
    var transport = endpointConfiguration.UseTransport<LearningTransport>();

    var routing = transport.Routing();
    routing.RouteToEndpoint(typeof(StartCoffeeMachine), "SmartHome.CoffeeMachine.Endpoint");

    endpointConfiguration.UseSerialization<SystemJsonSerializer>();
    endpointConfiguration.SendOnly();

    return endpointConfiguration;
});

services.AddDbContext<CoffeeMachineDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("coffeeMachineDbConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
