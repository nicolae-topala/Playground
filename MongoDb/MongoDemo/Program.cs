using Microsoft.Extensions.Options;
using MongoDemo.BLL.Services.Implementations;
using MongoDemo.BLL.Services.Interfaces;
using MongoDemo.Common;
using MongoDemo.Common.Interfaces;
using MongoDemo.DAL.Repositories.Implementations;
using MongoDemo.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.
services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));

services.AddSingleton<IMongoDbSettings>(serviceProvider =>
        serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
services.AddScoped<IUserService, UserService>();

services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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
