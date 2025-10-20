using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThreeLayerProject.Entities.Enums;

namespace ThreeLayerProject.Entities.Models
{
    public class Skill : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public SkillCategory Category { get; set; } = SkillCategory.Other;

        [Range(0, 100)]
        public int Proficiency { get; set; } = 0;

        public int? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<ProjectSkill>? ProjectSkills { get; set; }
    }
}
