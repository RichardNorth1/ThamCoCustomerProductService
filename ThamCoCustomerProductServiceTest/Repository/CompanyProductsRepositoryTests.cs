using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThamCoCustomerProductService.Data;
using ThamCoCustomerProductService.Repositiory;

namespace ThamCoCustomerProductService.Tests.Repository
{
    [TestFixture]
    public class CompanyProductsRepositoryTests
    {
        private ProductDbContext _context;
        private CompanyProductsRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ProductDbContext(options);
            _repository = new CompanyProductsRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CreateCompanyProducts_AddsCompanyProductToDatabase()
        {
            // Arrange
            var companyProduct = new CompanyProducts { CompanyId = 1, ProductId = 1 };

            // Act
            var result = await _repository.CreateCompanyProducts(companyProduct);

            // Assert
            Assert.AreEqual(companyProduct, result);
            Assert.AreEqual(1, _context.CompanyProducts.Count());
        }

        [Test]
        public async Task DeleteCompanyProducts_RemovesCompanyProductFromDatabase()
        {
            // Arrange
            var companyProduct = new CompanyProducts { CompanyId = 1, ProductId = 1 };
            _context.CompanyProducts.Add(companyProduct);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteCompanyProducts(1, 1);

            // Assert
            Assert.AreEqual(0, _context.CompanyProducts.Count());
        }

        [Test]
        public void DeleteCompanyProducts_ThrowsKeyNotFoundException_WhenCompanyProductNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.DeleteCompanyProducts(1, 1));
        }

        [Test]
        public async Task GetCompanyProducts_ReturnsListOfCompanyProducts()
        {
            // Arrange
            var companyProducts = new List<CompanyProducts>
            {
                new CompanyProducts { CompanyId = 1, ProductId = 1 },
                new CompanyProducts { CompanyId = 2, ProductId = 2 }
            };
            _context.CompanyProducts.AddRange(companyProducts);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCompanyProducts();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetCompanyProductsById_ReturnsCompanyProduct()
        {
            // Arrange
            var companyProduct = new CompanyProducts { CompanyId = 1, ProductId = 1 };
            _context.CompanyProducts.Add(companyProduct);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCompanyProductsById(1, 1);

            // Assert
            Assert.AreEqual(companyProduct, result);
        }

        [Test]
        public void GetCompanyProductsById_ThrowsKeyNotFoundException_WhenCompanyProductNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetCompanyProductsById(1, 1));
        }
    }
}
