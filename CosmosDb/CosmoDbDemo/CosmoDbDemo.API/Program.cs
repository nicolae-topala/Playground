using CosmoDbDemo.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CosmosDbContext>((serviceProvider, optionsBuilder) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var cosmosDbSettings = configuration.GetSection("CosmosDb");

    optionsBuilder.UseCosmos(
        cosmosDbSettings["Account"],
        cosmosDbSettings["Key"],
        cosmosDbSettings["DatabaseName"]);
});
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
