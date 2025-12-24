using ThreeLayerProject.Data.Interfaces; 
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.Data.Repositories
{
    public class ContactRepository : GenericRepository<ContactMessage>, IContactRepository
    {
        public ContactRepository(AppDbContext context) : base(context)
        {
        }
    }
}