using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Entities.Models;
using ThreeLayerProject.Entities.Enums;

namespace ThreeLayerProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<Experience> Experiences { get; set; } = null!;
        public DbSet<Education> Educations { get; set; } = null!;
        public DbSet<ContactMessage> ContactMessages { get; set; } = null!;
        public DbSet<ProjectSkill> ProjectSkills { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================
            // ContactMessage
            // ==========================
            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.ToTable("ContactMessages");
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Subject).HasMaxLength(200);
                entity.Property(c => c.Message).IsRequired();
                entity.Property(c => c.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(c => c.Status).HasDefaultValue(StatusEnum.Active);
            });

            // ==========================
            // Project
            // ==========================
            modelBuilder.Entity<Project>()
                .HasOne(p => p.User).WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

            // ==========================
            // Skill
            // ==========================
            modelBuilder.Entity<Skill>()
                .HasOne(s => s.User).WithMany(u => u.Skills)
                .HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Cascade);

            // ==========================
            // Experience
            // ==========================
            modelBuilder.Entity<Experience>()
                .HasOne(e => e.User).WithMany(u => u.Experiences)
                .HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);

            // ==========================
            // Education
            // ==========================
            modelBuilder.Entity<Education>()
                .HasOne(ed => ed.User).WithMany(u => u.Educations)
                .HasForeignKey(ed => ed.UserId).OnDelete(DeleteBehavior.Cascade);

            // ==========================
            // ProjectSkill
            // ==========================
            modelBuilder.Entity<ProjectSkill>()
                .HasKey(ps => new { ps.ProjectId, ps.SkillId });

            modelBuilder.Entity<ProjectSkill>()
                .HasOne(ps => ps.Project).WithMany(p => p.ProjectSkills)
                .HasForeignKey(ps => ps.ProjectId);

            modelBuilder.Entity<ProjectSkill>()
                .HasOne(ps => ps.Skill).WithMany(s => s.ProjectSkills)
                .HasForeignKey(ps => ps.SkillId);

            // ==========================
            // UserRole
            // ==========================
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Composite primary key

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
