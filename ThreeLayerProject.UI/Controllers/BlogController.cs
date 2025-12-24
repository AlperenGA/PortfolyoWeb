using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Controllers
{
    [Authorize] // Sadece giriş yapmış admin görebilir
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // Resim yükleme için

        public BlogController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // 1. LİSTELEME (INDEX)
        public async Task<IActionResult> Index()
        {
            // En son eklenen en üstte olacak şekilde getir
            var blogs = await _context.Blogs.OrderByDescending(b => b.CreatedDate).ToListAsync();
            return View(blogs);
        }

        // 2. EKLEME SAYFASI (CREATE GET)
        public IActionResult Create()
        {
            return View();
        }

        // 3. EKLEME İŞLEMİ (CREATE POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // Resim Yükleme
                if (file != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assetsAdmin", "img", "blog");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    blog.ImageUrl = "/assetsAdmin/img/blog/" + uniqueFileName;
                }

                blog.CreatedDate = DateTime.Now;
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // 4. DÜZENLEME SAYFASI (EDIT GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();

            return View(blog);
        }

        // 5. DÜZENLEME İŞLEMİ (EDIT POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog, IFormFile? file)
        {
            if (id != blog.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Yeni resim varsa güncelle
                    if (file != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assetsAdmin", "img", "blog");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        blog.ImageUrl = "/assetsAdmin/img/blog/" + uniqueFileName;
                    }
                    else
                    {
                        // Resim değişmediyse eskisini koru
                        var existingBlog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                        if (existingBlog != null) blog.ImageUrl = existingBlog.ImageUrl;
                    }

                    // Tarihi koruyalım, değişmesin
                    var originalDate = await _context.Blogs.AsNoTracking().Select(b => new { b.Id, b.CreatedDate }).FirstOrDefaultAsync(b => b.Id == id);
                    if (originalDate != null) blog.CreatedDate = originalDate.CreatedDate;

                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Blogs.Any(e => e.Id == blog.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // 6. SİLME İŞLEMİ
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}