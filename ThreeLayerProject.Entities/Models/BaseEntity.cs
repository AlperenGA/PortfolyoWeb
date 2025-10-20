using System;
using ThreeLayerProject.Entities.Enums;

namespace ThreeLayerProject.Entities.Models
{
    /// <summary>
    /// Tüm entity sınıflarının kalıtacağı temel sınıf.
    /// Ortak alanlar: Id, CreatedAt, UpdatedAt, Status, IsActive
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Active;
        public bool IsActive => Status == StatusEnum.Active;
    }
}
