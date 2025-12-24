using ThreeLayerProject.Entities.Models;

namespace Site.UI.Models
{
    public class HomePageViewModel
    {
        public AboutMe? AboutInfo { get; set; }           // Hakkımda Bilgisi (Başlık, Açıklama, Resim)
        public List<Project> RecentProjects { get; set; } = new List<Project>(); // Son Eklenen Projeler
        public List<Blog> RecentBlogs { get; set; } = new List<Blog>();       // Son Blog Yazıları
    }
}