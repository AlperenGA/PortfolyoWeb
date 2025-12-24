using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Veritabanı yoksa oluştur
            context.Database.EnsureCreated();

            // Eğer hiç kullanıcı yoksa, varsayılan Admin'i ekle
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Name = "Admin",
                    Surname = "User",
                    Email = "admin@admin.com",
                    // Not: Şimdilik şifreyi düz metin yazıyoruz. 
                    // İleride UI katmanında UserService içinde bunu MD5 veya SHA256 ile hashleyeceğiz.
                    // Şifre: 123456
                    PasswordHash = "123456", 
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
            
            // Varsayılan İletişim Bilgisi (Boş kalmasın)
            if (!context.ContactInfos.Any())
            {
                context.ContactInfos.Add(new ContactInfo
                {
                    Address = "İstanbul, Türkiye",
                    Email = "info@sirket.com",
                    Phone = "+90 555 000 0000",
                    MainHeading = "Birlikte Harika Projeler Yapalım",
                    MainDescription = "Modern çözümler ve yenilikçi tasarımlar.",
                    Stat1Text = "100+ Proje",
                    Stat2Text = "50+ Mutlu Müşteri"
                });
                context.SaveChanges();
            }
        }
    }
}