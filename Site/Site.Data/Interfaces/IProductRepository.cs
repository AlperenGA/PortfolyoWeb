using System.Collections.Generic;
using Site.Entities;

namespace Site.Data.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetProductById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
