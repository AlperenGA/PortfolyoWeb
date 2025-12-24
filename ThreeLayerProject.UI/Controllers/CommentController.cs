using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.UI.Models; // ViewModel burada

namespace ThreeLayerProject.UI.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;

        public CommentController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new CommentsViewModel
            {
                // İlişkili tabloları (Blog ve Project) Include ediyoruz ki başlıkları görelim
                BlogComments = await _context.BlogComments
                    .Include(c => c.Blog)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToListAsync(),

                ProjectComments = await _context.ProjectComments
                    .Include(c => c.Project)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // --- BLOG ACTIONS ---
        public async Task<IActionResult> ApproveBlogComment(int id)
        {
            var comment = await _context.BlogComments.FindAsync(id);
            if (comment != null)
            {
                comment.IsApproved = !comment.IsApproved; // Toggle
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteBlogComment(int id)
        {
            var comment = await _context.BlogComments.FindAsync(id);
            if (comment != null)
            {
                _context.BlogComments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // --- PROJECT ACTIONS ---
        public async Task<IActionResult> ApproveProjectComment(int id)
        {
            var comment = await _context.ProjectComments.FindAsync(id);
            if (comment != null)
            {
                comment.IsApproved = !comment.IsApproved; // Toggle
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProjectComment(int id)
        {
            var comment = await _context.ProjectComments.FindAsync(id);
            if (comment != null)
            {
                _context.ProjectComments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}