using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Entities;

namespace ThreeLayerProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<ContactMessage> ContactMessages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactMessage>()
                .ToTable("ContactMessages");

            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Subject).HasMaxLength(200);
                entity.Property(c => c.Message).IsRequired();
                entity.Property(c => c.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
