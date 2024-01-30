using TaskScheduling.API.Services;
using TaskScheduling.Shared.Interfaces;
using TaskScheduling.Shared.Services;
using TaskScheduling.Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddQuartzServices();

builder.Services.AddHostedService<WeatherCacheRefresherBackgroundService>();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, CacheService>();

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
