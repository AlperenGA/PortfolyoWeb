using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.Entities.Models
{
    public class ContactInfo : BaseEntity
    {
        // --- Ön Yüz Başlık ve Açıklamalar ---
        [MaxLength(200)]
        public string MainHeading { get; set; } = "Let's Build an Awesome Project Together";
        
        [MaxLength(500)]
        public string MainDescription { get; set; } = "Each demo built with Teba will look different...";

        // --- İstatistikler ---
        [MaxLength(50)]
        public string Stat1Text { get; set; } = "350+ Projects Done"; // Sol Kutu
        [MaxLength(50)]
        public string Stat2Text { get; set; } = "500+ Happy Clients"; // Sağ Kutu

        // --- İletişim Detayları ---
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string WorkingHours { get; set; } = string.Empty;
    }
}