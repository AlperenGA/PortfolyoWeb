using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 🔹 PostgreSQL bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Repository bağımlılıkları
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// 🔹 MVC servisi
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔹 Hata yönetimi ve güvenlik
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 🔹 HTTP pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🔹 Varsayılan route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
