using System.Threading.Tasks;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Interfaces
{
    public interface IUserService
    {
        // ======================
        // Admin kontrolü ve doğrulama
        // ======================
        User? GetAdminUser(); // DB’den admin kullanıcıyı getir
        User? ValidateUser(string username, string password); // Hash ile doğrulama
        Task<User?> ValidateUserAsync(string username, string password);
        bool IsAdmin(User user); // Kullanıcının admin olup olmadığını kontrol et

        // ======================
        // Eski kullanıcı yönetimi metodları
        // ======================
        User? GetUserById(int id);
        User? GetUserByEmail(string email);
        User? GetUserByUsername(string username);
        bool Register(User user, string password, string roleName = "User");
        bool UpdateProfile(User user);
        bool DeleteUser(int id);
        bool ChangePassword(int userId, string currentPassword, string newPassword);

        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> RegisterAsync(User user, string password, string roleName = "User");
        Task<bool> UpdateProfileAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }
}
