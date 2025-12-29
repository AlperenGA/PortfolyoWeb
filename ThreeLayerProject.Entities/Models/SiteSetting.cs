namespace ThreeLayerProject.Entities.Models
{
    public class SiteSetting : BaseEntity
    {
        public string? SiteTitle { get; set; } // Tarayıcı sekmesindeki başlık
        public string? MetaDescription { get; set; } // SEO Açıklaması
        
        public string? LogoUrl { get; set; } // Logo Yolu
        public string? FooterLogoUrl { get; set; } // Footer Logosu (Genelde beyaz olur)
        
        public string? FooterText { get; set; } // Footer'daki açıklama yazısı
        public string? CopyrightText { get; set; } // "Copyright 2025..."
        
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
    }
}