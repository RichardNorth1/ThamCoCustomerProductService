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
    public class CompanyRepositoryTests
    {
        private ProductDbContext _context;
        private CompanyRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ProductDbContext(options);
            _repository = new CompanyRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CreateCompany_AddsCompanyToDatabase()
        {
            // Arrange
            var company = new Company { CompanyId = 1, Name = "Test Company", Address = "123 Test St", Phone = "123-456-7890", Email = "test@example.com" };

            // Act
            var result = await _repository.CreateCompany(company);

            // Assert
            Assert.AreEqual(company, result);
            Assert.AreEqual(1, _context.Companies.Count());
        }

        [Test]
        public async Task DeleteCompany_RemovesCompanyFromDatabase()
        {
            // Arrange
            var company = new Company { CompanyId = 1, Name = "Test Company", Address = "123 Test St", Phone = "123-456-7890", Email = "test@example.com" };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteCompany(1);

            // Assert
            Assert.AreEqual(0, _context.Companies.Count());
        }

        [Test]
        public void DeleteCompany_ThrowsKeyNotFoundException_WhenCompanyNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.DeleteCompany(1));
        }

        [Test]
        public async Task GetCompanies_ReturnsListOfCompanies()
        {
            // Arrange
            var companies = new List<Company>
            {
                new Company { CompanyId = 1, Name = "Test Company 1", Address = "123 Test St", Phone = "123-456-7890", Email = "test1@example.com" },
                new Company { CompanyId = 2, Name = "Test Company 2", Address = "456 Test St", Phone = "987-654-3210", Email = "test2@example.com" }
            };
            _context.Companies.AddRange(companies);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCompanies();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetCompanyById_ReturnsCompany()
        {
            // Arrange
            var company = new Company { CompanyId = 1, Name = "Test Company", Address = "123 Test St", Phone = "123-456-7890", Email = "test@example.com" };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCompanyById(1);

            // Assert
            Assert.AreEqual(company, result);
        }

        [Test]
        public void GetCompanyById_ThrowsKeyNotFoundException_WhenCompanyNotFound()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetCompanyById(1));
        }

        [Test]
        public async Task UpdateCompany_UpdatesCompanyInDatabase()
        {
            // Arrange
            var company = new Company { CompanyId = 1, Name = "Test Company", Address = "123 Test St", Phone = "123-456-7890", Email = "test@example.com" };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            var updatedCompany = new Company { CompanyId = 1, Name = "Updated Company", Address = "456 Updated St", Phone = "987-654-3210", Email = "updated@example.com" };

            // Act
            var result = await _repository.UpdateCompany(updatedCompany);

            // Assert
            Assert.AreEqual(updatedCompany.Name, result.Name);
            Assert.AreEqual(updatedCompany.Address, result.Address);
            Assert.AreEqual(updatedCompany.Phone, result.Phone);
            Assert.AreEqual(updatedCompany.Email, result.Email);
        }

        [Test]
        public void UpdateCompany_ThrowsKeyNotFoundException_WhenCompanyNotFound()
        {
            // Arrange
            var updatedCompany = new Company { CompanyId = 1, Name = "Updated Company", Address = "456 Updated St", Phone = "987-654-3210", Email = "updated@example.com" };

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.UpdateCompany(updatedCompany));
        }
    }
}
