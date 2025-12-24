using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data;
using ThreeLayerProject.Entities.Models;
using ThreeLayerProject.UI.Interfaces;

namespace ThreeLayerProject.UI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            // NOT: Gerçek senaryoda şifreler Hash'li tutulur. 
            // Şimdilik DataSeeder'da düz metin "123456" verdiğimiz için düz kontrol ediyoruz.
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);

            return user;
        }
    }
}