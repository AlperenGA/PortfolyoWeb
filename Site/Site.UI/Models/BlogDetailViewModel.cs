using ThreeLayerProject.Entities.Models;

namespace Site.UI.Models
{
    public class BlogDetailViewModel
    {
        // Blog içeriği ve onaylanmış yorumlar
        public Blog? Blog { get; set; }

        // Yeni yorum göndermek için
        public BlogComment NewComment { get; set; } = new BlogComment();
    }
}