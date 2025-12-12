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
                new Product { Id = 1, Title = "Gaming Laptop", Description = "High performance laptop", Price = 2999.99M, ImageUrl = "/images/laptop.jpg", Categories = "Hardware", Client = "ClientA", Owner = "Alperen Alev", Website = "https://example.com" },
                new Product { Id = 2, Title = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 59.99M, ImageUrl = "/images/mouse.jpg", Categories = "Peripherals", Client = "ClientB", Owner = "Alperen Alev", Website = "https://example.com" },
                new Product { Id = 3, Title = "Mechanical Keyboard", Description = "RGB mechanical keyboard", Price = 129.99M, ImageUrl = "/images/keyboard.jpg", Categories = "Peripherals", Client = "ClientC", Owner = "Alperen Alev", Website = "https://example.com" }
            };
        }

        public IEnumerable<Product> GetAllProducts() => _products;

        public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existing = GetProductById(product.Id);
            if (existing != null)
            {
                existing.Title = product.Title;
                existing.Description = product.Description;
                existing.Price = product.Price;
                existing.ImageUrl = product.ImageUrl;
                existing.Categories = product.Categories;
                existing.Client = product.Client;
                existing.Owner = product.Owner;
                existing.Website = product.Website;
                existing.StartDate = product.StartDate;
                existing.EndDate = product.EndDate;
            }
        }

        public void Delete(int id)
        {
            var product = GetProductById(id);
            if (product != null)
                _products.Remove(product);
        }
    }
}
