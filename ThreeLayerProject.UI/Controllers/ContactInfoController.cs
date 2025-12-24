using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Controllers
{
    [Authorize]
    public class ContactInfoController : Controller
    {
        private readonly AppDbContext _context;

        public ContactInfoController(AppDbContext context)
        {
            _context = context;
        }

        // Tek bir sayfa olacak, varsa getir yoksa boş form
        public async Task<IActionResult> Index()
        {
            var info = await _context.ContactInfos.FirstOrDefaultAsync();
            if (info == null)
            {
                // Veritabanında henüz kayıt yoksa yeni bir tane oluşturup kaydedelim ki ID'si olsun
                info = new ContactInfo();
                _context.Add(info);
                await _context.SaveChangesAsync();
            }
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactInfo info)
        {
            if (ModelState.IsValid)
            {
                _context.Update(info);
                await _context.SaveChangesAsync();
                TempData["Success"] = "İletişim bilgileri başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(info);
        }
    }
}