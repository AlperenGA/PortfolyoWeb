using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using Site.UI.Models;
using ThreeLayerProject.Entities.Models;

namespace Site.UI.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        // BLOG LİSTESİ (Index)
        public async Task<IActionResult> Index()
        {
            // Yayında olan blogları, tarihe göre yeniden eskiye sırala
            var blogs = await _context.Blogs
                                      .Where(b => b.IsPublished)
                                      .OrderByDescending(b => b.CreatedDate)
                                      .ToListAsync();
            return View(blogs);
        }

        // BLOG DETAYI (Details)
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _context.Blogs
                                     .Include(b => b.Comments) // Yorumları çek
                                     .FirstOrDefaultAsync(b => b.Id == id && b.IsPublished);

            if (blog == null) return NotFound();

            var viewModel = new BlogDetailViewModel
            {
                Blog = blog
            };

            return View(viewModel);
        }

        // YORUM GÖNDERME (Post Comment)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(BlogDetailViewModel model)
        {
            // BlogId boş gelmesin diye hidden field'dan alıyoruz
            var blogId = model.NewComment.BlogId;

            if (ModelState.IsValid) // Basit validasyon (Ad, Email, Yorum dolu mu?)
            {
                var comment = model.NewComment;
                comment.CreatedDate = DateTime.Now;
                comment.IsApproved = false; // Yorumlar admin onayı bekler

                _context.BlogComments.Add(comment);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Yorumunuz gönderildi ve onay bekliyor.";
                return RedirectToAction(nameof(Details), new { id = blogId });
            }

            // Hata varsa sayfayı tekrar yükle
            var blog = await _context.Blogs
                                     .Include(b => b.Comments)
                                     .FirstOrDefaultAsync(b => b.Id == blogId);
            
            model.Blog = blog;
            return View("Details", model);
        }
    }
}