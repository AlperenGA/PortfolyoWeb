using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;
using System.IO;
using System.Threading.Tasks;
using BCrypt.Net;

namespace ThreeLayerProject.UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Admin session kontrolü
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Index", "Login");

            // Admin kullanıcı DB’den çekiliyor
            var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == "admin");
            if (adminUser == null)
            {
                TempData["ErrorMessage"] = "Admin user not found.";
                return RedirectToAction("Index", "Login");
            }

            return View(adminUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(int id, string firstName, string lastName, IFormFile? profileImage)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Index", "Login");

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            user.FirstName = firstName;
            user.LastName = lastName;

            // Profil resmi yükleme
            if (profileImage != null && profileImage.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(profileImage.FileName)}";
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }

                user.AvatarUrl = "/images/profiles/" + fileName;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(int id, string oldPassword, string newPassword)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Index", "Login");

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            // Şifre doğrulama (hash kontrolü)
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
            {
                TempData["ErrorMessage"] = "Old password is incorrect.";
                return RedirectToAction("Index");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToAction("Index");
        }
    }
}
