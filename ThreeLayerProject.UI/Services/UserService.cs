using Microsoft.EntityFrameworkCore;
using ThreeLayerProject.UI.Interfaces;
using ThreeLayerProject.Entities.Models;
using ThreeLayerProject.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BCrypt.Net;

namespace ThreeLayerProject.UI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // ======================
        // Admin kullanıcı
        // ======================
        public User? GetAdminUser()
        {
            return _context.Users.FirstOrDefault(u => u.Username == "admin");
        }

        public User? ValidateUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return null;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }

        public bool IsAdmin(User user)
        {
            return user != null && user.Username == "admin";
        }

        // ======================
        // Kullanıcı CRUD (DB uyumlu)
        // ======================
        public async Task<User?> GetUserByIdAsync(int id)
            => await _context.Users.FindAsync(id);

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetUserByUsernameAsync(string username)
            => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<bool> RegisterAsync(User user, string password, string roleName = "User")
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email || u.Username == user.Username))
                return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProfileAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.Id);
            if (existing == null) return false;

            existing.FirstName = user.FirstName;
            existing.LastName = user.LastName;
            existing.Email = user.Email;
            existing.AvatarUrl = user.AvatarUrl;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        // ======================
        // Sync versiyonlar
        // ======================
        public User? GetUserById(int id) => GetUserByIdAsync(id).Result;
        public User? GetUserByEmail(string email) => GetUserByEmailAsync(email).Result;
        public User? GetUserByUsername(string username) => GetUserByUsernameAsync(username).Result;
        public bool Register(User user, string password, string roleName = "User") => RegisterAsync(user, password, roleName).Result;
        public bool UpdateProfile(User user) => UpdateProfileAsync(user).Result;
        public bool DeleteUser(int id) => DeleteUserAsync(id).Result;
        public bool ChangePassword(int userId, string currentPassword, string newPassword) => ChangePasswordAsync(userId, currentPassword, newPassword).Result;
    }
}
