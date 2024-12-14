using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThamCoCustomerProductService.Data;
using ThamCoCustomerProductService.Dtos;
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
            var companyProduct = new CompanyProducts
            {
                CompanyId = 1,
                ProductId = 1,
                Price = 10,
                StockLevel = 10
            };

            var result = await _repository.CreateCompanyProducts(companyProduct);

            Assert.AreEqual(companyProduct, result);
            Assert.AreEqual(1, _context.CompanyProducts.Count());
        }

        [Test]
        public async Task DeleteCompanyProducts_RemovesCompanyProductFromDatabase()
        {
            var companyProduct = new CompanyProducts
            {
                CompanyId = 1,
                ProductId = 1,
                Price = 10,
                StockLevel = 10
            };
            _context.CompanyProducts.Add(companyProduct);
            await _context.SaveChangesAsync();

            await _repository.DeleteCompanyProducts(1, 1);

            Assert.AreEqual(0, _context.CompanyProducts.Count());
        }

        [Test]
        public void DeleteCompanyProducts_ThrowsKeyNotFoundException_WhenCompanyProductNotFound()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.DeleteCompanyProducts(1, 1));
        }

        [Test]
        public async Task GetCompanyProducts_ReturnsListOfCompanyProducts()
        {
            var companyProducts = new List<CompanyProducts>
            {
                new CompanyProducts {
                    CompanyId = 1,
                    ProductId = 1,
                    Price = 10,
                    StockLevel = 10
                } ,
                new CompanyProducts {
                    CompanyId = 2,
                    ProductId = 2,
                    Price = 20,
                    StockLevel = 20
                }
            };
            _context.CompanyProducts.AddRange(companyProducts);
            await _context.SaveChangesAsync();

            var result = await _repository.GetCompanyProducts();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetCompanyProductsById_ReturnsCompanyProduct()
        {
            var companyProduct = new CompanyProducts
            {
                CompanyId = 1,
                ProductId = 1,
                Price = 10,
                StockLevel = 10
            };
            _context.CompanyProducts.Add(companyProduct);
            await _context.SaveChangesAsync();

            var result = await _repository.GetCompanyProductsById(1, 1);

            Assert.AreEqual(companyProduct, result);
        }

        [Test]
        public void GetCompanyProductsById_ThrowsKeyNotFoundException_WhenCompanyProductNotFound()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetCompanyProductsById(1, 1));
        }
    }
}
