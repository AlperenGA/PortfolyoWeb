using ThreeLayerProject.Entities;
using System.Threading.Tasks;

namespace ThreeLayerProject.Data.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactMessage>> GetAllAsync();
        Task<ContactMessage?> GetByIdAsync(int id);
        Task AddAsync(ContactMessage message);
    }
}
