namespace ThreeLayerProject.Entities.Models
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; } // Marka Adı (Alt Text için)
        public string ImageUrl { get; set; } // Logo Yolu
        public int Order { get; set; } // Sıralama
    }
}