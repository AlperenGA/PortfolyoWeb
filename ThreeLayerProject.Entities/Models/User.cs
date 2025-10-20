using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class User : BaseEntity
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? AvatarUrl { get; set; }

        [MaxLength(1000)]
        public string? Bio { get; set; }

        // Navigations
        public ICollection<Project>? Projects { get; set; }
        public ICollection<Experience>? Experiences { get; set; }
        public ICollection<Education>? Educations { get; set; }
        public ICollection<Skill>? Skills { get; set; }
    }
}
