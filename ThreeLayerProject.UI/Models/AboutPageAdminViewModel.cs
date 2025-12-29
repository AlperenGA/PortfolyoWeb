using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Models
{
    public class AboutPageAdminViewModel
    {
        // Hakkımızda Yazıları ve Resmi
        public AboutMe AboutMe { get; set; }

        // Marka Logoları Listesi
        public List<Brand> Brands { get; set; }
    }
}