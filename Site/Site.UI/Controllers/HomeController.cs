using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using Site.UI.Models;
using ThreeLayerProject.Entities.Models;

namespace Site.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomePageViewModel
            {
                // 1. Hakkımda Bilgisini Çek (İlk kayıt)
                AboutInfo = await _context.AboutMe.FirstOrDefaultAsync(),

                // 2. Son 3 Projeyi Çek (Tarihe göre azalan)
                RecentProjects = await _context.Projects
                    .Where(p => p.IsPublished)
                    .OrderByDescending(p => p.ProjectDate)
                    .Take(3) // Sadece 3 tane
                    .ToListAsync(),

                // 3. Son 3 Blog Yazısını Çek (Tarihe göre azalan)
                RecentBlogs = await _context.Blogs
                    .Where(b => b.IsPublished)
                    .OrderByDescending(b => b.CreatedDate)
                    .Take(3) // Sadece 3 tane
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}