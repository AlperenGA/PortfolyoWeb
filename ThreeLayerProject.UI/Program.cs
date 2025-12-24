using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.UI.Services;
using ThreeLayerProject.UI.Interfaces;
// Repository ve Interface namespace'lerini ekledik
using ThreeLayerProject.Data.Interfaces;
using ThreeLayerProject.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı Yolu
var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ThreeLayerProject.Data", "ThreeLayerProject.db");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// 2. GÜVENLİK AYARLARI
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/Index";
        options.Cookie.Name = "PortfolyoAdminCookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });

// 3. SERVİSLERİN KAYDI (Dependency Injection)
builder.Services.AddScoped<IUserService, UserService>();

// Repository kaydı (Artık hata vermez çünkü Interface ve Class eşleşiyor)
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// 4. SEED DATA
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try 
    {
        DataSeeder.Seed(context); 
    }
    catch(Exception ex)
    {
        Console.WriteLine("Seed Hatası: " + ex.Message);
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();