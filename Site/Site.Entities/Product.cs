using System;

namespace Site.Entities
{
    public class Product
    {
        public int Id { get; set; }

        // Controller ve view'larda kullanılan 'Title' alanı
        public string Title { get; set; } = string.Empty;

        // (isteğe bağlı) eski isim uyumluluğu gerekirse Name property ekleyebilirsin:
        // public string Name { get => Title; set => Title = value; }

        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Diğer alanlar (proje bilgisi gibi)
        public string Client { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string Owner { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Categories
        {
        get => Category;
        set => Category = value;
        }
        public string Website { get; set; } = string.Empty;
    }
}
