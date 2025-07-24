using System.Data;
using LogBg3Armory.Models;
using LogBg3Armory.Repositories;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ✅ Inject MySQL connection + your repository
builder.Services.AddTransient<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // You can adjust HSTS for production
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ✅ This replaces .MapStaticAssets()

app.UseRouting();
app.UseAuthorization();

// ✅ Point default route to ItemController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Item}/{action=Index}/{id?}");

app.Run();