using BlazorMediatR.BLL;
using BlazorMediatR.BLL.Notifications.Customers;
using BlazorMediatR.DAL;
using BlazorMediatR.DAL.Interfaces;
using BlazorMediatR.DAL.Repositories;
using BlazorMediatR.UI.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>(); 

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BLLAssemblyMarker).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorMediatR.UI.Client._Imports).Assembly);

app.Run();
