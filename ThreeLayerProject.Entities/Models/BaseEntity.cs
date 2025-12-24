using System;
using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true; // Veriyi silmek yerine pasife çekmek için
    }
}