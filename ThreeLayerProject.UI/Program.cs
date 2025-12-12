using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Data.Interface;
using ThreeLayerProject.Data.Repositories;
using ThreeLayerProject.Entities.Models;
using ThreeLayerProject.UI.Services;
using ThreeLayerProject.UI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// SQLite DB
var dbPath = "/Users/kumo/Desktop/ThreeLayerProject/ThreeLayerProject.Data/ThreeLayerProject.db";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Service kayıtları
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// MVC ve Session
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// ==========================
// Database migration & seed
// ==========================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // DB migration
    db.Database.Migrate();

    // DataSeeder ile admin ve roller
    DataSeeder.Seed(db);

    Console.WriteLine($"Database path: {db.Database.GetDbConnection().DataSource}");
}

// Hata sayfaları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
