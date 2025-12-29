using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Controllers
{
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BrandController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // LİSTELEME
        public async Task<IActionResult> Index()
        {
            var brands = await _context.Brands.OrderBy(x => x.Order).ToListAsync();
            return View(brands);
        }

        // EKLEME (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // EKLEME (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand model, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                // Resim Yükleme İşlemi
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/brands");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                model.ImageUrl = "/uploads/brands/" + uniqueFileName;
            }
            else
            {
                // Resim zorunlu olsun istemiyorsan burayı geçebilirsin ama marka için resim şarttır genelde.
                ModelState.AddModelError("imageFile", "Lütfen bir logo yükleyin.");
                return View(model);
            }

            model.CreatedDate = DateTime.Now;
            model.IsActive = true;

            _context.Brands.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DÜZENLEME (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        // DÜZENLEME (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand model, IFormFile? imageFile)
        {
            var existing = await _context.Brands.FindAsync(model.Id);
            if (existing == null) return NotFound();

            existing.Name = model.Name;
            existing.Order = model.Order;
            existing.IsActive = model.IsActive;

            // Eğer yeni resim seçildiyse güncelle
            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/brands");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                existing.ImageUrl = "/uploads/brands/" + uniqueFileName;
            }

            _context.Brands.Update(existing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // SİLME
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}