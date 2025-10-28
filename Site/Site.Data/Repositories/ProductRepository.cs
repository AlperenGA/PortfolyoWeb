using Site.Data.Interfaces;
using Site.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Site.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            // Test verisi – veritabanı yerine geçici olarak bellekte tutuluyor
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Gaming Laptop", Description = "High performance laptop", Price = 2999.99M, ImageUrl = "/images/laptop.jpg" },
                new Product { Id = 2, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 59.99M, ImageUrl = "/images/mouse.jpg" },
                new Product { Id = 3, Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard", Price = 129.99M, ImageUrl = "/images/keyboard.jpg" }
            };
        }

        public IEnumerable<Product> GetAll() => _products;

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Description = product.Description;
                existing.Price = product.Price;
                existing.ImageUrl = product.ImageUrl;
            }
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
                _products.Remove(product);
        }
    }
}
