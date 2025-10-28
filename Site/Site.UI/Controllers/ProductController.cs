using Microsoft.AspNetCore.Mvc;
using Site.Data;
using Site.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Site.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // --- LIST / INDEX ---
        public IActionResult Index()
        {
            IEnumerable<Product> products;

            try
            {
                products = _productRepository.GetAllProducts();
            }
            catch
            {
                // Repository yoksa veya hata olursa örnek veriyle devam et
                products = GetSampleProducts();
            }

            return View(products);
        }

        // --- DETAILS / SINGLE ITEM ---
        public IActionResult Details(int id)
        {
            Product? product = null;

            try
            {
                product = _productRepository.GetProductById(id);
            }
            catch
            {
                // fallback içinden id’ye göre bul
                product = GetSampleProducts().FirstOrDefault(p => p.Id == id);
            }

            if (product == null)
                return NotFound();

            return View(product);
        }

        // --- LOCAL FALLBACK METHOD ---
        private IEnumerable<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Title = "Business Development", Description = "Comprehensive business growth strategy.", Categories = "Digital Marketing", ImageUrl = "/~/assets/img/project/card/1.jpg", Client = "TechVision", Owner = "Alperen Alev", Website = "https://example.com" },
                new Product { Id = 2, Title = "Plan Development", Description = "Planning and execution consulting.", Categories = "Consulting", ImageUrl = "/~/assets/img/project/card/2.jpg", Client = "NextGen", Owner = "Alperen Alev", Website = "https://example.com" },
                new Product { Id = 3, Title = "Risk Management", Description = "Analyzing and mitigating project risks.", Categories = "New Business", ImageUrl = "/~/assets/img/project/card/3.jpg", Client = "BizSafe", Owner = "Alperen Alev", Website = "https://example.com" },
                new Product { Id = 4, Title = "Investment Idea", Description = "Innovative financial strategy design.", Categories = "Digital Marketing", ImageUrl = "/~/assets/img/project/card/4.jpg", Client = "InvestCo", Owner = "Alperen Alev", Website = "https://example.com" }
            };
        }
    }
}
