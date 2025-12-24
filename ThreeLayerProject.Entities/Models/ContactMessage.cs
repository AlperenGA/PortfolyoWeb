using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class ContactMessage : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false; // Okundu/Okunmadı bildirimi için
    }
}