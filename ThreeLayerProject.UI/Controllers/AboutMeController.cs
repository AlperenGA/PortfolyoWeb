using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Controllers
{
    [Authorize]
    public class AboutMeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // Resim yükleme için gerekli

        public AboutMeController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Veritabanındaki ilk kaydı çek
            var about = await _context.AboutMe.FirstOrDefaultAsync();

            // EĞER VERİ YOKSA (Veritabanı sıfırlandığı için):
            // Hata vermek yerine, View'a içi boş yeni bir AboutMe nesnesi gönderiyoruz.
            // Böylece sayfa açılır ve sen bilgileri doldurup kaydedebilirsin.
            return View(about ?? new AboutMe());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AboutMe model, IFormFile? file)
        {
            // Model validasyonunu kontrol et (Resim zorunlu değilse ModelState'den çıkarılabilir)
            // Ancak AboutMe tablosu genelde esnek olduğu için direkt işleme geçiyoruz.

            // 1. Veritabanında kayıt var mı kontrol et
            var existingAbout = await _context.AboutMe.FirstOrDefaultAsync();

            // 2. Resim Yükleme İşlemi
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assetsAdmin", "img", "about");
                
                // Klasör yoksa oluştur
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Yeni resim yolunu modele ata
                model.ImageUrl = "/assetsAdmin/img/about/" + uniqueFileName;
            }
            else
            {
                // Yeni resim yüklenmediyse ve eski bir kayıt varsa, eski resmi koru
                if (existingAbout != null)
                {
                    model.ImageUrl = existingAbout.ImageUrl;
                }
            }

            // 3. Kaydetme veya Güncelleme
            if (existingAbout == null)
            {
                // Hiç kayıt yoksa YENİ EKLE
                model.CreatedDate = DateTime.Now;
                model.IsActive = true;
                _context.Add(model);
            }
            else
            {
                // Kayıt varsa GÜNCELLE
                // ID'yi eşitlememiz lazım ki EF Core hangisini güncelleyeceğini bilsin
                model.Id = existingAbout.Id; 
                model.CreatedDate = existingAbout.CreatedDate; // Oluşturulma tarihini koru
                
                // EF Core'un takip ettiği 'existingAbout' yerine 'model' değerlerini veritabanına aktarıyoruz
                _context.Entry(existingAbout).CurrentValues.SetValues(model);
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Hakkımda bilgileri başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
    }
}