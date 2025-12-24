using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.UI.Interfaces
{
    public interface IUserService
    {
        // Login işlemi için asenkron metod tanımı
        Task<User?> LoginAsync(string email, string password);
    }
}