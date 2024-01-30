using GraphQL.BLL.Mutations;
using GraphQL.BLL.Queries;
using GraphQL.BLL.Subscriptions;
using GraphQL.Common;
using GraphQL.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddPooledDbContextFactory<SqlDbContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddSorting()
    .AddFiltering()
    .RegisterDbContext<SqlDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

builder.Services.RegisterMapsterConfiguration();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseWebSockets();
app.MapGraphQL();

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
