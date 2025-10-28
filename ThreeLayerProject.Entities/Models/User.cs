using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? AvatarUrl { get; set; }

        [MaxLength(1000)]
        public string? Bio { get; set; }

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        // Rol ilişkisi - admin-only mantığında tek role ihtiyacımız var
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        // Navigations - opsiyonel, admin panelinde kullanmak isteyebilirsin
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }

    public class Role : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        // Sadece admin için UserRole koleksiyonu yeterli
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    public class UserRole
    {
        public int UserId { get; set; }
        public User? User { get; set; } // nullable bırakmak güvenli

        public int RoleId { get; set; }
        public Role? Role { get; set; } // nullable bırakmak güvenli
    }
}
