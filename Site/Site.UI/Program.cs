using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Site.Data;
using Site.Data.Interfaces;
using Site.Data.Repositories;
using Site.Entities;  // Burada Entities katmanını dahil ettik.
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// ============================
// 💠 SERVICE CONFIGURATION
// ============================

// MVC (Controller + View)
builder.Services.AddControllersWithViews();

// Database (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository registration
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// SMTP Client (Mail gönderimi için) - appsettings.json'da EmailSettings olduğundan emin ol
var emailSettings = builder.Configuration.GetSection("EmailSettings");
if (!string.IsNullOrWhiteSpace(emailSettings["SmtpServer"]))
{
    builder.Services.AddSingleton<SmtpClient>(_ =>
        new SmtpClient(emailSettings["SmtpServer"]!)
        {
            Port = int.TryParse(emailSettings["Port"], out var p) ? p : 587,
            Credentials = new NetworkCredential(emailSettings["FromEmail"] ?? string.Empty, emailSettings["Password"] ?? string.Empty),
            EnableSsl = true
        }
    );
}

// ============================
// 💠 BUILD APPLICATION
// ============================
var app = builder.Build();

// ============================
// 💠 MIDDLEWARE CONFIGURATION
// ============================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ============================
// 💠 ROUTING
// ============================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// ============================
// 💠 DATABASE MIGRATION
// ============================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ============================
// 💠 RUN
// ============================
app.Run();
