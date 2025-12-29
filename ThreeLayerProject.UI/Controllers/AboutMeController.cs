using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;
using ThreeLayerProject.UI.Models; // ViewModel için ekledik

namespace ThreeLayerProject.UI.Controllers
{
    [Authorize]
    public class AboutMeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutMeController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // ==========================================
        // 1. SAYFAYI GÖRÜNTÜLEME (GET)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1. Hakkımızda bilgisini çek
            var about = await _context.AboutMe.FirstOrDefaultAsync();

            // 2. Markaları çek (Sıralı)
            var brands = await _context.Brands.OrderBy(x => x.Order).ToListAsync();

            // 3. Hepsini ViewModel'e paketle
            var model = new AboutPageAdminViewModel
            {
                AboutMe = about ?? new AboutMe(), // Veri yoksa boş nesne gönder
                Brands = brands ?? new List<Brand>()
            };

            return View(model);
        }

        // ==========================================
        // 2. HAKKIMIZDA BİLGİSİNİ GÜNCELLEME (POST)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAbout(AboutMe aboutMe, IFormFile? file)
        {
            // Veritabanındaki mevcut kaydı bul
            var existingAbout = await _context.AboutMe.FirstOrDefaultAsync();

            // --- RESİM YÜKLEME ---
            if (file != null)
            {
                // Klasör yolu: wwwroot/assetsAdmin/img/about
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assetsAdmin", "img", "about");
                
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                aboutMe.ImageUrl = "/assetsAdmin/img/about/" + uniqueFileName;
            }
            else
            {
                // Yeni resim yoksa eskisini koru
                if (existingAbout != null)
                {
                    aboutMe.ImageUrl = existingAbout.ImageUrl;
                }
            }

            // --- KAYDETME ---
            if (existingAbout == null)
            {
                // İlk defa ekleniyorsa
                aboutMe.CreatedDate = DateTime.Now;
                aboutMe.IsActive = true;
                _context.AboutMe.Add(aboutMe);
            }
            else
            {
                // Güncelleme
                existingAbout.FullName = aboutMe.FullName;
                existingAbout.ShortDescription = aboutMe.ShortDescription;
                existingAbout.LongDescription = aboutMe.LongDescription;
                existingAbout.TeamSectionTitle = aboutMe.TeamSectionTitle; // İstediğin başlık alanı
                existingAbout.ImageUrl = aboutMe.ImageUrl;
                
                _context.AboutMe.Update(existingAbout);
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Hakkımızda bilgileri güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        // ==========================================
        // 3. YENİ MARKA LOGOSU EKLEME (POST)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBrand(string name, int order, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                // Klasör yolu: wwwroot/assetsAdmin/img/brand
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assetsAdmin", "img", "brand");
                
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                var newBrand = new Brand
                {
                    Name = name,
                    Order = order,
                    ImageUrl = "/assetsAdmin/img/brand/" + uniqueFileName,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                _context.Brands.Add(newBrand);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Yeni marka eklendi.";
            }
            else
            {
                TempData["Error"] = "Lütfen bir logo dosyası seçin.";
            }

            return RedirectToAction(nameof(Index));
        }

        // ==========================================
        // 4. MARKA SİLME (POST)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Marka silindi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}