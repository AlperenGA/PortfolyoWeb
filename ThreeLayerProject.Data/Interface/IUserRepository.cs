using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.Data.Interface
{
    public interface IUserRepository
    {
        // Admin login işlemleri
        Task<User?> GetAdminUserAsync();
        Task<User?> ValidateUserAsync(string username, string password);

        // Opsiyonel: sync versiyonlar
        User? GetAdminUser();
        User? ValidateUser(string username, string password);

        // Diğer CRUD metodları devre dışı
        Task<User?> GetByIdAsync(int id) => Task.FromResult<User?>(null);
        Task<List<User>> GetAllAsync() => Task.FromResult(new List<User>());
        Task AddAsync(User user) { return Task.CompletedTask; }
        Task UpdateAsync(User user) { return Task.CompletedTask; }
        Task DeleteAsync(User user) { return Task.CompletedTask; }
    }
}
