using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;

namespace ThreeLayerProject.UI.Controllers
{
    [Authorize] // Sadece giriş yapmış admin görebilir
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Toplam Proje Sayısı
            var totalProjects = await _context.Projects.CountAsync();

            // 2. Toplam Blog Yazısı Sayısı (View'da kullanmak istersen diye)
            var totalBlogs = await _context.Blogs.CountAsync();

            // 3. Okunmamış Mesaj Sayısı (IsRead == false olanlar)
            var unreadMessages = await _context.ContactMessages.CountAsync(x => !x.IsRead);

            // 4. Onay Bekleyen Yorum Sayısı (Blog ve Proje yorumlarının toplamı)
            var pendingBlogComments = await _context.BlogComments.CountAsync(x => !x.IsApproved);
            var pendingProjectComments = await _context.ProjectComments.CountAsync(x => !x.IsApproved);
            var totalPendingComments = pendingBlogComments + pendingProjectComments;

            // Bu verileri ViewBag ile View'a taşıyoruz
            ViewBag.TotalProjectsCount = totalProjects;
            ViewBag.TotalBlogsCount = totalBlogs;
            ViewBag.UnreadMessagesCount = unreadMessages;
            ViewBag.PendingCommentsCount = totalPendingComments;

            return View();
        }
    }
}