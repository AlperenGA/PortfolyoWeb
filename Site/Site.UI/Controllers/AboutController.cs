using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models; 

namespace Site.UI.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Veritabanındaki ilk kaydı getir. Yoksa hata vermemesi için boş nesne oluştur.
            var about = await _context.AboutMe.FirstOrDefaultAsync();
            return View(about ?? new AboutMe());
        }
    }
}