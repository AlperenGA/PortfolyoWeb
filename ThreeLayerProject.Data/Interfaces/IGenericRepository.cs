using System.Linq.Expressions;
using ThreeLayerProject.Entities.Models;

namespace ThreeLayerProject.Data.Interfaces // <-- BURASI DA 's' İLE BİTMELİ
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // ... içerik aynı kalsın
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}