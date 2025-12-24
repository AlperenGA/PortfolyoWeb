using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.Data.Interfaces  // <-- SONUNDA 's' OLMALI
{
    // Artık aynı namespace içinde oldukları için IGenericRepository'yi tanıyacak
    public interface IContactRepository : IGenericRepository<ContactMessage>
    {
        // Özel metodlar gerekirse buraya eklenir
    }
}