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
            var product = new Product { 
                ProductId = 1, 
                Name = "Test Product", 
                Brand = "Test Brand", 
                Description = "Test Description", 
                ImageUrl = "Test url" };

            var result = await _repository.CreateProduct(product);

            Assert.AreEqual(product, result);
            Assert.AreEqual(1, _context.Products.Count());
        }

        [Test]
        public async Task DeleteProduct_RemovesProductFromDatabase()
        {
            var product = new Product { 
                ProductId = 1, 
                Name = "Test Product", 
                Brand = "Test Brand", 
                Description = "Test Description", 
                ImageUrl = "Test url"
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            await _repository.DeleteProduct(1);

            Assert.AreEqual(0, _context.Products.Count());
        }

        [Test]
        public void DeleteProduct_ThrowsKeyNotFoundException_WhenProductNotFound()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.DeleteProduct(1));
        }

        [Test]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            var products = new List<Product>
            {
                new Product { 
                    ProductId = 1, 
                    Name = "Test Product 1", 
                    Brand = "Test Brand 1", 
                    Description = "Test Description 1", 
                    ImageUrl = "Test url" 
                },
                new Product { 
                    ProductId = 2, 
                    Name = "Test Product 2", 
                    Brand = "Test Brand 2", 
                    Description = "Test Description 2", 
                    ImageUrl = "Test url 2" }
            };
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            var result = await _repository.GetProducts();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetProductsById_ReturnsProduct()
        {
            var product = new Product { 
                ProductId = 1, 
                Name = "Test Product", 
                Brand = "Test Brand", 
                Description = "Test Description", 
                ImageUrl = "Test url"
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var result = await _repository.GetProductsById(1);

            Assert.AreEqual(product, result);
        }

        [Test]
        public void GetProductsById_ThrowsKeyNotFoundException_WhenProductNotFound()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetProductsById(1));
        }

        [Test]
        public async Task UpdateProduct_UpdatesProductInDatabase()
        {
            var product = new Product {
                ProductId = 1, 
                Name = "Test Product", 
                Brand = "Test Brand", 
                Description = "Test Description", 
                ImageUrl = "Test url"
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var updatedProduct = new Product { 
                ProductId = 1, 
                Name = "Updated Product", 
                Brand = "Updated Brand", 
                Description = "Updated Description", 
                ImageUrl = "Updated url"
            };

            var result = await _repository.UpdateProduct(updatedProduct);

            Assert.AreEqual(updatedProduct.Name, result.Name);
            Assert.AreEqual(updatedProduct.Brand, result.Brand);
            Assert.AreEqual(updatedProduct.Description, result.Description);
            Assert.AreEqual(updatedProduct.ImageUrl, result.ImageUrl);
        }

        [Test]
        public void UpdateProduct_ThrowsKeyNotFoundException_WhenProductNotFound()
        {
            var updatedProduct = new Product { 
                ProductId = 1, 
                Name = "Updated Product", 
                Brand = "Updated Brand", 
                Description = "Updated Description", 
                ImageUrl = "Test url"
            };

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.UpdateProduct(updatedProduct));
        }
    }
}
