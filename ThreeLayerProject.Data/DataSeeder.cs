using System;
using System.Linq;
using ThreeLayerProject.Entities.Models;
using ThreeLayerProject.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace ThreeLayerProject.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Migration'ları uygula
            context.Database.Migrate();

            // ==========================
            // Rol kontrol ve ekleme
            // ==========================
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (adminRole == null)
            {
                adminRole = new Role { Name = "Admin" };
                context.Roles.Add(adminRole);
                context.SaveChanges();
                Console.WriteLine("✅ Admin rolü eklendi.");
            }

            // ==========================
            // Admin kullanıcı kontrol ve ekleme
            // ==========================
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "System",
                    LastName = "Administrator",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
                    AvatarUrl = "/images/profiles/default.png",
                    Status = StatusEnum.Active,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                context.Users.Add(adminUser);
                context.SaveChanges();

                // Admin rolünü ata
                context.UserRoles.Add(new UserRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                });
                context.SaveChanges();

                Console.WriteLine("✅ Admin kullanıcısı başarıyla eklendi.");
            }
            else
            {
                Console.WriteLine("ℹ️ Admin kullanıcısı zaten mevcut, seed atlandı.");
            }
        }
    }
}
