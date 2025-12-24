using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models; 

namespace Site.UI.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        // LİSTELEME SAYFASI
        public async Task<IActionResult> Index()
        {
            // Sadece yayında olanları (IsPublished), tarihe göre sıralı getir
            var projects = await _context.Projects
                                         .Where(p => p.IsPublished)
                                         .OrderByDescending(p => p.ProjectDate)
                                         .ToListAsync();
            return View(projects);
        }

        // DETAY SAYFASI
        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects
                                        .FirstOrDefaultAsync(p => p.Id == id && p.IsPublished);

            if (project == null) return NotFound();

            return View(project);
        }
    }
}