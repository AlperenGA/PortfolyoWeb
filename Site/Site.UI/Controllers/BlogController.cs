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
    // 1. BlogId kontrolü
    if (model.NewComment.BlogId <= 0)
    {
        Console.WriteLine("HATA: BlogId 0 veya boş geldi!");
        return RedirectToAction("Index"); // Hata olursa listeye dön
    }

    // 2. Model Doğrulama (Ad, Email, Yorum boş mu?)
    // Not: ModelState.IsValid kontrolünü geçici olarak esnetiyoruz ki hatayı görelim.
    if (!string.IsNullOrEmpty(model.NewComment.FullName) && 
        !string.IsNullOrEmpty(model.NewComment.CommentText))
    {
        var comment = new BlogComment
        {
            BlogId = model.NewComment.BlogId,
            FullName = model.NewComment.FullName,
            Email = model.NewComment.Email,
            CommentText = model.NewComment.CommentText,
            CreatedDate = DateTime.Now,
            IsActive = true,
            IsApproved = false // ÖNEMLİ: Varsayılan olarak onay bekler
        };

        _context.BlogComments.Add(comment);
        await _context.SaveChangesAsync();

        Console.WriteLine("BAŞARILI: Yorum veritabanına kaydedildi.");
        TempData["Success"] = "Yorumunuz alındı, onaylandıktan sonra yayınlanacaktır.";
        
        return RedirectToAction("Details", new { id = model.NewComment.BlogId });
    }
    else
    {
        // Hata varsa terminale yazalım
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"VALIDATION HATASI: {error.ErrorMessage}");
        }
    }

    // Hata varsa sayfaya geri dön
    return RedirectToAction("Details", new { id = model.NewComment.BlogId });
}
    }
}