using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeLayerProject.Entities.Models; // ✅ Doğru namespace

namespace ThreeLayerProject.Data.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactMessage>> GetAllAsync();
        Task<ContactMessage?> GetByIdAsync(int id);
        Task AddAsync(ContactMessage message);
    }
}
