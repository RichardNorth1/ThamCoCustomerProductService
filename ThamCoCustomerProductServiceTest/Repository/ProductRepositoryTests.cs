using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThamCoCustomerProductService.Data;
using ThamCoCustomerProductService.Repositiory;

namespace ThamCoCustomerProductService.Tests.Repository
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private ProductDbContext _context;
        private ProductRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ProductDbContext(options);
            _repository = new ProductRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CreateProduct_AddsProductToDatabase()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product", Brand = "Test Brand", Description = "Test Description", ImageUrl = "http://example.com/image.jpg" };

            // Act
            var result = await _repository.CreateProduct(product);

            // Assert
            Assert.AreEqual(product, result);
            Assert.AreEqual(1, _context.Products.Count());
        }

        [Test]
        public async Task DeleteProduct_RemovesProductFromDatabase()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product", Brand = "Test Brand", Description = "Test Description", ImageUrl = "http://example.com/image.jpg" };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteProduct(1);

            // Assert
            Assert.AreEqual(0, _context.Products.Count());
        }

        [Test]
        public void DeleteProduct_ThrowsKeyNotFoundException_WhenProductNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.DeleteProduct(1));
        }

        [Test]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Test Product 1", Brand = "Test Brand 1", Description = "Test Description 1", ImageUrl = "http://example.com/image1.jpg" },
                new Product { ProductId = 2, Name = "Test Product 2", Brand = "Test Brand 2", Description = "Test Description 2", ImageUrl = "http://example.com/image2.jpg" }
            };
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProducts();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetProductsById_ReturnsProduct()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product", Brand = "Test Brand", Description = "Test Description", ImageUrl = "http://example.com/image.jpg" };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProductsById(1);

            // Assert
            Assert.AreEqual(product, result);
        }

        [Test]
        public void GetProductsById_ThrowsKeyNotFoundException_WhenProductNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetProductsById(1));
        }

        [Test]
        public async Task UpdateProduct_UpdatesProductInDatabase()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product", Brand = "Test Brand", Description = "Test Description", ImageUrl = "http://example.com/image.jpg" };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var updatedProduct = new Product { ProductId = 1, Name = "Updated Product", Brand = "Updated Brand", Description = "Updated Description", ImageUrl = "http://example.com/updated_image.jpg" };

            // Act
            var result = await _repository.UpdateProduct(updatedProduct);

            // Assert
            Assert.AreEqual(updatedProduct.Name, result.Name);
            Assert.AreEqual(updatedProduct.Brand, result.Brand);
            Assert.AreEqual(updatedProduct.Description, result.Description);
            Assert.AreEqual(updatedProduct.ImageUrl, result.ImageUrl);
        }

        [Test]
        public void UpdateProduct_ThrowsKeyNotFoundException_WhenProductNotFound()
        {
            // Arrange
            var updatedProduct = new Product { ProductId = 1, Name = "Updated Product", Brand = "Updated Brand", Description = "Updated Description", ImageUrl = "http://example.com/updated_image.jpg" };

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.UpdateProduct(updatedProduct));
        }
    }
}
