using Microsoft.EntityFrameworkCore;
using SystemRepairCar;
using SystemRepairCar.Events;
using SystemRepairCar.Interfaces;
using SystemRepairCar.Repository;
using SystemRepairCar.Services;
using SystemRepairCar.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarEventHandler, CarEventHandler>();
builder.Services.AddScoped<CarEventDispatcher>();
builder.Services.AddTransient<BlobService>();
builder.Services.AddTransient<ICarFactory, CarFactory>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Car}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "events",
    pattern: "{controller=Event}/{action=Index}/{id?}");

await app.RunAsync();
