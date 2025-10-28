using Microsoft.EntityFrameworkCore;
using Site.UI.Models;

namespace Site.Data
{
    public class AppDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
