using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ThreeLayerProject.UI.Interfaces;
using ThreeLayerProject.Entities.ViewModels;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill in all required fields.";
                return View(model);
            }

            // Admin user DB'den çekiliyor
            var adminUser = _userService.GetAdminUser();
            if (adminUser == null)
            {
                TempData["ErrorMessage"] = "Admin user not found. Contact system administrator.";
                return View(model);
            }

            // Hash’li şifre kontrolü
            var validUser = _userService.ValidateUser(adminUser.Username, model.Password);
            if (validUser != null)
            {
                HttpContext.Session.SetString("UserId", adminUser.Id.ToString());
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("IsLoggedIn", "true");

                TempData["SuccessMessage"] = $"Welcome, {adminUser.FirstName}!";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Invalid username or password.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Index");
        }
    }
}
