using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThreeLayerProject.Entities.Models
{
    public class BlogComment : BaseEntity
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

        public bool IsApproved { get; set; } = false; // Admin onaylamadan sitede görünmeyecek

        // Hangi Blog'a yapıldı?
        public int BlogId { get; set; }
        
        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; } = null!;
    }
}