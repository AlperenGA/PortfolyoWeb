using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Controllers
{
    public class SiteSettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SiteSettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Veritabanındaki ilk ve tek ayarı getir
            var setting = await _context.SiteSettings.FirstOrDefaultAsync();
            
            // Eğer hiç ayar yoksa boş bir model gönder (View'da yeni oluştururuz)
            if (setting == null)
            {
                setting = new SiteSetting();
            }
            
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SiteSetting model, IFormFile? logoFile, IFormFile? footerLogoFile)
        {
            // 1. Üst Logo Yükleme İşlemi
            if (logoFile != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + logoFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await logoFile.CopyToAsync(fileStream);
                }
                model.LogoUrl = "/uploads/" + uniqueFileName;
            }

            // 2. Footer Logo Yükleme İşlemi
            if (footerLogoFile != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + footerLogoFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await footerLogoFile.CopyToAsync(fileStream);
                }
                model.FooterLogoUrl = "/uploads/" + uniqueFileName;
            }

            // 3. Veritabanı İşlemi (Ekleme veya Güncelleme)
            if (model.Id == 0)
            {
                // Yeni Kayıt
                _context.SiteSettings.Add(model);
            }
            else
            {
                // Güncelleme
                var existing = await _context.SiteSettings.FindAsync(model.Id);
                if (existing != null)
                {
                    existing.SiteTitle = model.SiteTitle;
                    existing.MetaDescription = model.MetaDescription;
                    existing.FooterText = model.FooterText;
                    existing.CopyrightText = model.CopyrightText;
                    existing.PhoneNumber = model.PhoneNumber;
                    existing.EmailAddress = model.EmailAddress;
                    existing.Address = model.Address;

                    // Eğer yeni resim yüklenmediyse eskisi kalsın, yüklendiyse yenisi gelsin
                    if (model.LogoUrl != null) existing.LogoUrl = model.LogoUrl;
                    if (model.FooterLogoUrl != null) existing.FooterLogoUrl = model.FooterLogoUrl;
                    
                    _context.SiteSettings.Update(existing);
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Site ayarları başarıyla güncellendi.";
            
            return RedirectToAction("Index");
        }
    }
}