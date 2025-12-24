using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data; // AppDbContext'in olduğu namespace (Data katmanını referans aldık)

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı Bağlantısı (Admin Paneliyle AYNI veritabanı olmalı)
// appsettings.json dosyasından "DefaultConnection" ismini okur.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. MVC Servislerini Ekle (Controller ve View kullanımı için)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. Hata Yönetimi ve Güvenlik (Production ortamı için)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Varsayılan HSTS değeri 30 gündür. Production için artırılabilir.
    app.UseHsts();
}

app.UseHttpsRedirection();

// 4. Statik Dosyalar (CSS, JS, Resimler - wwwroot klasörü için şart)
app.UseStaticFiles();

app.UseRouting();

// 5. Yetkilendirme (Şu an site tarafında login yok ama standart kalabilir)
app.UseAuthorization();

// 6. Rotalama (Varsayılan olarak Home/Index açılır)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();