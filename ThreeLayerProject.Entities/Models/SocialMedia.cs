namespace ThreeLayerProject.Entities.Models
{
    public class SocialMedia : BaseEntity
    {
        public string Title { get; set; } // Örn: Facebook
        public string Link { get; set; } // Örn: https://facebook.com/sayfa
        public string IconCode { get; set; } // SVG kodu veya class ismi (fa fa-facebook)
        public int Order { get; set; } // Sıralama
    }
}