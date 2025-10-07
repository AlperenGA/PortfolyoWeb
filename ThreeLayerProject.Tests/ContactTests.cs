using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerProject.Data;
using ThreeLayerProject.Data.Repositories;
using ThreeLayerProject.Entities;
using ThreeLayerProject.UI.Controllers;
using ThreeLayerProject.UI.Models;
using Xunit;

namespace ThreeLayerProject.Tests
{
    public class ContactTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task Repository_AddGetAllGetById_Test()
        {
            var context = GetInMemoryDbContext();
            var repo = new ContactRepository(context);

            var message = new ContactMessage
            {
                Name = "Test User",
                Email = "test@example.com",
                Subject = "Hello",
                Message = "This is a test",
                CreatedAt = DateTime.UtcNow
            };

            await repo.AddAsync(message);

            var allMessages = await repo.GetAllAsync();
            Assert.Single(allMessages);
            Console.WriteLine($"GetAllAsync: {allMessages.First().Name} - {allMessages.First().Email}");

            var retrieved = await repo.GetByIdAsync(message.Id);
            Assert.NotNull(retrieved);
            Console.WriteLine($"GetByIdAsync: {retrieved!.Name} - {retrieved.Email}");
        }

        [Fact]
        public async Task Controller_IndexPost_ValidModel_Test()
        {
            var context = GetInMemoryDbContext();
            var repo = new ContactRepository(context);
            var controller = new ContactController(repo)
            {
                TempData = new TempDataDictionary(new DefaultHttpContext(), new MockTempDataProvider())
            };

            var model = new ContactViewModel
            {
                Name = "Controller Test",
                Email = "controller@test.com",
                Subject = "Test",
                Message = "Controller message test"
            };

            var result = await controller.Index(model) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result!.ActionName);

            var dbMessage = await repo.GetAllAsync();
            Assert.Single(dbMessage);
            Console.WriteLine($"Controller Add: {dbMessage.First().Name} - {dbMessage.First().Email}");
        }

        [Fact]
        public async Task Controller_IndexPost_InvalidModel_Test()
        {
            var context = GetInMemoryDbContext();
            var repo = new ContactRepository(context);
            var controller = new ContactController(repo)
            {
                TempData = new TempDataDictionary(new DefaultHttpContext(), new MockTempDataProvider())
            };

            controller.ModelState.AddModelError("Name", "Required");
            var model = new ContactViewModel(); // boş model

            var result = await controller.Index(model) as ViewResult;

            Assert.NotNull(result);
            Assert.False(controller.ModelState.IsValid);
            Console.WriteLine("Controller returned ViewResult due to invalid model.");
        }
    }

    // 🔧 TempData için basit mock sınıfı
    public class MockTempDataProvider : ITempDataProvider
    {
        private readonly Dictionary<string, object> _data = new();

        public IDictionary<string, object> LoadTempData(HttpContext context) => _data;

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
            foreach (var kvp in values)
                _data[kvp.Key] = kvp.Value;
        }
    }
}
