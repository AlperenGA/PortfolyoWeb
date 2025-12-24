using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Models
{
    public class CommentsViewModel
    {
        // Blog yorumlarını tutacak liste
        public List<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

        // Proje yorumlarını tutacak liste
        public List<ProjectComment> ProjectComments { get; set; } = new List<ProjectComment>();
    }
}