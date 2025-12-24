using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class Project : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Category { get; set; } = string.Empty; // Örn: Web, Mobile, Branding

        public string? ImageUrl { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Client { get; set; } = string.Empty;
        
        public string ProjectDate { get; set; } = string.Empty; // Örn: "14 July, 2025"

        public string WebsiteUrl { get; set; } = string.Empty;

        public bool IsPublished { get; set; } = true;

        // İlişki: Bir projenin birden fazla yorumu olabilir
        public virtual ICollection<ProjectComment> Comments { get; set; } = new List<ProjectComment>();
    }
}