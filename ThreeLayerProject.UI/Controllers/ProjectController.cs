using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Controllers
{
    [Authorize] // Sadece giriş yapmış admin görebilir
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // Resim yükleme için

        public ProjectController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        

        // LİSTELEME
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.OrderByDescending(x => x.CreatedDate).ToListAsync();
            return View(projects);
        }

        // EKLEME SAYFASI (GET)
        public IActionResult Create()
        {
            return View();
        }

        // EKLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // Resim Yükleme İşlemi
                if (file != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assetsAdmin", "img", "projects");
                    // Klasör yoksa oluştur
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    project.ImageUrl = "/assetsAdmin/img/projects/" + uniqueFileName;
                }

                project.CreatedDate = DateTime.Now;
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // DÜZENLEME SAYFASI (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        // DÜZENLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project, IFormFile? file)
        {
            if (id != project.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Yeni resim seçildiyse güncelle
                    if (file != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assetsAdmin", "img", "projects");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        project.ImageUrl = "/assetsAdmin/img/projects/" + uniqueFileName;
                    }
                    else
                    {
                        // Resim değişmediyse eskisini koru (Bunu yapmazsak null olur)
                        // Bu basit örnekte veritabanından tekrar çekip set etmemiz lazım veya hidden input kullanmalıyız
                        // Pratik yol: AsNoTracking kullanmadan önce mevcut veriyi çekmek.
                        var existingProject = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                        if (existingProject != null) project.ImageUrl = existingProject.ImageUrl;
                    }

                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Projects.Any(e => e.Id == project.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // SİLME
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}