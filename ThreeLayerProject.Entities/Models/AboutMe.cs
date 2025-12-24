using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class AboutMe : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ShortDescription { get; set; } = string.Empty; // Ünvan vb.

        public string LongDescription { get; set; } = string.Empty; // Biyografi

        public string? ImageUrl { get; set; }
    }
}