using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class Blog : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public bool IsPublished { get; set; } = false; // Taslak/Yayında durumu

        // İlişki: Bir blog yazısının birden fazla yorumu olabilir
        public virtual ICollection<BlogComment> Comments { get; set; } = new List<BlogComment>();
    }
}