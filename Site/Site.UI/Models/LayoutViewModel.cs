using ThreeLayerProject.Entities.Models;

namespace Site.UI.Models
{
    public class LayoutViewModel
    {
        public SiteSetting? Setting { get; set; }
        public List<SocialMedia>? SocialMedias { get; set; }
    }
}