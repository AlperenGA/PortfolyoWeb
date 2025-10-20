using System;
using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class Education : BaseEntity
    {
        [Required, MaxLength(200)]
        public string School { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Degree { get; set; }

        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
