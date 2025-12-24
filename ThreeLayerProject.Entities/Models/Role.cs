using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class Role : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty; // "Admin", "Editor" vb.

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}