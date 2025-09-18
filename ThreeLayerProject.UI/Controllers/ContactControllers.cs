using Microsoft.AspNetCore.Mvc;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities;
using ThreeLayerProject.UI.Models;

namespace ThreeLayerProject.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ContactMessage
                {
                    Name = model.Name,
                    Email = model.Email,
                    Subject = model.Subject,
                    Message = model.Message
                };

                _context.ContactMessages.Add(entity);
                _context.SaveChanges();

                ViewBag.Success = "Your message has been sent. Thank you!";
                return View(new ContactViewModel());
            }

            return View(model);
        }
    }
}
