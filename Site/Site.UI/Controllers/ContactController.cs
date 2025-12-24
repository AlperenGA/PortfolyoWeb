using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using Site.UI.Models;
using ThreeLayerProject.Entities.Models;

namespace Site.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        // SAYFAYI GÖSTER (GET)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ContactPageViewModel
            {
                // Veritabanındaki iletişim bilgilerini çek
                ContactInfo = await _context.ContactInfos.FirstOrDefaultAsync()
            };

            return View(viewModel);
        }

        // MESAJ GÖNDER (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactPageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = model.ContactMessage;
                
                // Gerekli sistem alanlarını doldur
                message.CreatedDate = DateTime.Now;
                message.IsRead = false; // Yeni mesaj okunmadı olarak başlar
                message.IsActive = true;

                _context.ContactMessages.Add(message);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Mesajınız başarıyla gönderildi. En kısa sürede dönüş yapacağız.";
                return RedirectToAction(nameof(Index));
            }

            // Hata varsa bilgileri tekrar çekip sayfayı geri döndür
            model.ContactInfo = await _context.ContactInfos.FirstOrDefaultAsync();
            return View("Index", model);
        }
    }
}