using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // --- AUTH (Giriş) TABLOLARI ---
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        // --- İÇERİK YÖNETİMİ TABLOLARI ---
        public DbSet<Project> Projects { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Brand> Brands { get; set; }

        // --- YORUM TABLOLARI ---
        public DbSet<BlogComment> BlogComments { get; set; }       // <-- EKLENDİ
        public DbSet<ProjectComment> ProjectComments { get; set; } // <-- EKLENDİ
        
        // --- LAYOUT TABLOLARI ---
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Kullanıcı ve Rol İlişkileri
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // İlişkilerin tanımlanması (Cascade Delete vb. ayarlar)
            
            // Blog silinirse yorumları da silinsin
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.Blog)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // Proje silinirse yorumları da silinsin
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Project)
                .HasForeignKey(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}