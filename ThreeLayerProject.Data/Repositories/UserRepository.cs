using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.Data.Interface;
using ThreeLayerProject.Entities.Models;
using BCrypt.Net;

namespace ThreeLayerProject.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // ======================
        // Admin kullanıcı
        // ======================
        public async Task<User?> GetAdminUserAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles!)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserRoles.Any(ur => ur.Role!.Name == "Admin"));
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles!)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null) return null;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }

        // ======================
        // Sync versiyonlar
        // ======================
        public User? GetAdminUser() => GetAdminUserAsync().Result;
        public User? ValidateUser(string username, string password) => ValidateUserAsync(username, password).Result;

        // ======================
        // Diğer metodlar devre dışı
        // ======================
        public async Task<User?> GetByIdAsync(int id) => null;
        public async Task<List<User>> GetAllAsync() => new List<User>();
        public async Task AddAsync(User user) { /* Devre dışı */ }
        public async Task UpdateAsync(User user) { /* Devre dışı */ }
        public async Task DeleteAsync(User user) { /* Devre dışı */ }
    }
}
