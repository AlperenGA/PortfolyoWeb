using System;
using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities
{
    public class ContactMessage
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Form -> name

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty; // Form -> email

        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty; // Form -> subject

        [Required]
        public string Message { get; set; } = string.Empty; // Form -> message

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Kayıt zamanı
    }
}
