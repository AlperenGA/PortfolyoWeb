using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThreeLayerProject.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactMessage>> GetAllAsync()
        {
            return await _context.ContactMessages.ToListAsync();
        }

        public async Task<ContactMessage?> GetByIdAsync(int id)
        {
            return await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(ContactMessage message)
        {
            await _context.ContactMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
