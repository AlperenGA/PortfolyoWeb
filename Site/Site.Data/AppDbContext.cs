using Microsoft.EntityFrameworkCore;

namespace Site.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Şu anda hiçbir DbSet yok çünkü e-posta veritabanına kaydedilmiyor.
    }
}
