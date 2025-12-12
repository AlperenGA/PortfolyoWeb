using Microsoft.AspNetCore.Mvc;
using Site.Data.Interfaces;
using Site.Entities;
using Site.UI.Models;
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
            IEnumerable<Site.Entities.Product> entityProducts;

            try
            {
                entityProducts = _productRepository.GetAllProducts();
            }
            catch
            {
                entityProducts = GetSampleProductsEntities(); // fallback (örnek veri)
            }

            // 🔹 Entities → UI.Models dönüşümü
            var uiProducts = entityProducts.Select(p => new Site.UI.Models.Product
            {
                Id = p.Id,
                Title = p.Title,
                Category = p.Category,         // <-- artık var
                Description = p.Description,
                ImageUrl = p.ImageUrl
            }).ToList();

            return View(uiProducts);
        }

        // --- DETAILS / SINGLE ITEM ---
        public IActionResult Details(int id)
        {
            Site.Entities.Product? entityProduct = null;

            try
            {
                entityProduct = _productRepository.GetProductById(id);
            }
            catch
            {
                entityProduct = GetSampleProductsEntities().FirstOrDefault(p => p.Id == id);
            }

            if (entityProduct == null)
                return NotFound();

            // 🔹 Entities → UI.Models dönüşümü
            var uiModel = new Site.UI.Models.Product
            {
                Id = entityProduct.Id,
                Title = entityProduct.Title,
                Category = entityProduct.Category,
                Description = entityProduct.Description,
                ImageUrl = entityProduct.ImageUrl
            };

            return View(uiModel);
        }

        // --- LOCAL FALLBACK METHOD (Entities tipinde veri) ---
        private IEnumerable<Site.Entities.Product> GetSampleProductsEntities()
        {
            return new List<Site.Entities.Product>
            {
                new Site.Entities.Product 
                { 
                    Id = 1, 
                    Title = "Business Development", 
                    Description = "Comprehensive business growth strategy.", 
                    Category = "Digital Marketing", 
                    ImageUrl = "~/assets/img/project/card/1.jpg" 
                },
                new Site.Entities.Product 
                { 
                    Id = 2, 
                    Title = "Plan Development", 
                    Description = "Planning and execution consulting.", 
                    Category = "Consulting", 
                    ImageUrl = "~/assets/img/project/card/2.jpg" 
                },
                new Site.Entities.Product 
                { 
                    Id = 3, 
                    Title = "Risk Management", 
                    Description = "Analyzing and mitigating project risks.", 
                    Category = "New Business", 
                    ImageUrl = "~/assets/img/project/card/3.jpg" 
                },
                new Site.Entities.Product 
                { 
                    Id = 4, 
                    Title = "Investment Idea", 
                    Description = "Innovative financial strategy design.", 
                    Category = "Finance", 
                    ImageUrl = "~/assets/img/project/card/4.jpg" 
                }
            };
        }
    }
}
