using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using Site.UI.Models;

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
            // 1. Hakkımızda bilgisini çek
            var about = await _context.AboutMe.FirstOrDefaultAsync();

            // 2. Markaları (Logoları) çek (Sırasıyla)
            var brands = await _context.Brands
                                       .Where(x => x.IsActive)
                                       .OrderBy(x => x.Order)
                                       .ToListAsync();

            // 3. ViewModel'i doldur
            var model = new AboutPageViewModel
            {
                AboutInfo = about,
                Brands = brands
            };

            return View(model);
        }
    }
}