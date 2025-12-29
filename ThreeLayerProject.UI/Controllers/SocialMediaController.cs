using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Controllers
{
    public class SocialMediaController : Controller
    {
        private readonly AppDbContext _context;

        public SocialMediaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.SocialMedias.OrderBy(x => x.Order).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMedia model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.IsActive = true;
                _context.SocialMedias.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var social = await _context.SocialMedias.FindAsync(id);
            if (social == null) return NotFound();
            return View(social);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SocialMedia model)
        {
            if (ModelState.IsValid)
            {
                _context.SocialMedias.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var social = await _context.SocialMedias.FindAsync(id);
            if (social != null)
            {
                _context.SocialMedias.Remove(social);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}