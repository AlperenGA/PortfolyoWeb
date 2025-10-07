using Microsoft.AspNetCore.Mvc;
using ThreeLayerProject.Data.Repositories;
using ThreeLayerProject.Entities;
using ThreeLayerProject.UI.Models;
using System;
using System.Threading.Tasks;

namespace ThreeLayerProject.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        // GET: /Contact/
        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContactViewModel());
        }

        // POST: /Contact/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (model == null) 
                throw new ArgumentNullException(nameof(model));

            if (!ModelState.IsValid)
                return View(model);

            var entity = new ContactMessage
            {
                Name = model.Name ?? string.Empty,
                Email = model.Email ?? string.Empty,
                Subject = model.Subject ?? string.Empty,
                Message = model.Message ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            await _contactRepository.AddAsync(entity);

            TempData["SuccessMessage"] = "Your message has been sent. Thank you!";
            return RedirectToAction(nameof(Index));
        }
    }
}
