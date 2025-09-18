using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Entities;

namespace ThreeLayerProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            
            modelBuilder.Entity<ContactMessage>().ToTable("ContactMessages");
        }
    }
}
