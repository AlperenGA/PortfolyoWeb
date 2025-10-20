using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    /// <summary>
    /// Explicit many-to-many join entity (Project - Skill).
    /// İleride örneğin kullanım yüzdesi veya açıklama eklemek istersen esneklik sağlar.
    /// </summary>
    [Table("ProjectSkills")]
    public class ProjectSkill : BaseEntity
    {
        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project? Project { get; set; }

        [Required]
        public int SkillId { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill? Skill { get; set; }

        /// <summary>Projedeki skill'in önem derecesi (0-100)</summary>
        [Range(0, 100)]
        public int Weight { get; set; } = 0;
    }
}
