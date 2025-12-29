using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders; // Bu gerekli!
using ThreeLayerProject.Data;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı Bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<Site.UI.Services.LayoutService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// 1. Kendi wwwroot klasörünü kullan (CSS/JS için)
app.UseStaticFiles();

// 2. Admin Panelinin wwwroot klasörünü SANKİ BURADAYMIŞ GİBİ kullan

var adminWwwRootPath = "/Users/kumo/Desktop/ThreeLayerProject/ThreeLayerProject.UI/wwwroot";

if (Directory.Exists(adminWwwRootPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(adminWwwRootPath),
        // RequestPath vermiyoruz, böylece dosyalar kök dizindeymiş gibi davranır.
        
    });
}

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();