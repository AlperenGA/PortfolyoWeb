using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data; // Admin Veritabanı
using ThreeLayerProject.Entities.Models;
using Site.UI.Models; // ViewModel burada

namespace Site.UI.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                                         .Where(p => p.IsPublished)
                                         .OrderByDescending(p => p.ProjectDate)
                                         .ToListAsync();
            return View(projects);
        }

        // DETAY SAYFASI (GÜNCELLENDİ)
        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects
                                        .Include(p => p.Comments) // Yorumları da getir
                                        .FirstOrDefaultAsync(p => p.Id == id && p.IsPublished);

            if (project == null) return NotFound();

            var viewModel = new ProjectDetailViewModel
            {
                Project = project
            };

            return View(viewModel);
        }

        // YORUM EKLEME (YENİ METOT)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(ProjectDetailViewModel model)
        {
            var projectId = model.NewComment.ProjectId;
            
            if (projectId <= 0) return RedirectToAction("Index");

            if (!string.IsNullOrEmpty(model.NewComment.FullName) && 
                !string.IsNullOrEmpty(model.NewComment.CommentText))
            {
                var comment = new ProjectComment
                {
                    ProjectId = projectId,
                    FullName = model.NewComment.FullName,
                    Email = model.NewComment.Email,
                    CommentText = model.NewComment.CommentText,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsApproved = false // Onay bekler
                };

                _context.ProjectComments.Add(comment);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Yorumunuz alındı, onaylandıktan sonra görünecektir.";
                return RedirectToAction("Details", new { id = projectId });
            }

            // Hata varsa sayfayı tekrar yükle
            var project = await _context.Projects
                                        .Include(p => p.Comments)
                                        .FirstOrDefaultAsync(p => p.Id == projectId);
            
            model.Project = project;
            return View("Details", model);
        }
    }
}