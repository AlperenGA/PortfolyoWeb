using Microsoft.AspNetCore.Mvc;

namespace Site.UI.Controllers
{
    public class HomeController : Controller
    {
        // Ana sayfa (default)
        public IActionResult Index()
        {
            return View();
        }

        // İstersen ileride About, Contact gibi alt sayfalar da buraya gelebilir
        public IActionResult About()
        {
            return View();
        }
    }
}
