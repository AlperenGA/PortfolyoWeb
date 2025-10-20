using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class Project : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? Url { get; set; }

        [MaxLength(200)]
        public string? RepoUrl { get; set; }

        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<ProjectSkill>? ProjectSkills { get; set; }
    }
}
