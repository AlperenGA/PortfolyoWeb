using Microsoft.EntityFrameworkCore;
using Site.Data; // AppDbContext burada
using Site.UI.Services; // Sadece IProductRepository ve ProductRepository için

var builder = WebApplication.CreateBuilder(args);

// --- SERVICES ---
builder.Services.AddControllersWithViews();

// --- DATABASE ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
    // SQL Server için: .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// --- CUSTOM SERVICES ---
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// --- MIDDLEWARE ---
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

// --- ROUTING ---
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// --- MIGRATION / SEED ---
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
