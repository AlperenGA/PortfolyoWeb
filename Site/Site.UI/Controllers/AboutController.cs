using Microsoft.AspNetCore.Mvc;

namespace Site.UI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            // Views/About/Index.cshtml dosyasını döndürür
            return View();
        }
    }
}
