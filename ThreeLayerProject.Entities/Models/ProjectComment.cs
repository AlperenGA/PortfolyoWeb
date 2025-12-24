using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThreeLayerProject.Entities.Models
{
    public class ProjectComment : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string CommentText { get; set; } = string.Empty;

        public bool IsApproved { get; set; } = false; // Admin onayı

        // Hangi Projeye yapıldı?
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; } = null!;
    }
}