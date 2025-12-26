using ThreeLayerProject.Entities.Models;

namespace Site.UI.Models
{
    public class ProjectDetailViewModel
    {
        public Project? Project { get; set; }
        public ProjectComment NewComment { get; set; } = new ProjectComment();
    }
}