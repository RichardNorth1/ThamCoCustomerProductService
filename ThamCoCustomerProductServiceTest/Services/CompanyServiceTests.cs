using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThamCoCustomerProductService.Data;
using ThamCoCustomerProductService.Dtos;
using ThamCoCustomerProductService.Repositiory;
using ThamCoCustomerProductService.Services;

namespace ThamCoCustomerProductService.Tests.Services
{
    [TestFixture]
    public class CompanyServiceTests
    {
        private Mock<ICompanyRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private CompanyService _service;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<ICompanyRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CompanyService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CreateCompany_CallsRepositoryWithMappedEntity()
        {
            // Arrange
            var companyDto = new CompanyDto { CompanyId = 1, Name = "Test Company", Address = "123 Test St", Phone = "123-456-7890", Email = "test@example.com" };
            var company = new Company { CompanyId = 1, Name = "Test Company", Address = "123 Test St", Phone = "123-456-7890", Email = "test@example.com" };

            _mockMapper.Setup(m => m.Map<Company>(companyDto)).Returns(company);

            // Act
            await _service.CreateCompany(companyDto);

            // Assert
            _mockRepository.Verify(r => r.CreateCompany(company), Times.Once);
        }

        [Test]
        public void DeleteCompany_ThrowsKeyNotFoundException_WhenCompanyIdIsNull()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.DeleteCompany(0));
        }

        [Test]
        public async Task DeleteCompany_CallsRepositoryWithCorrectId()
        {
            // Act
            await _service.DeleteCompany(1);

            // Assert
            _mockRepository.Verify(r => r.DeleteCompany(1), Times.Once);
        }

        [Test]
        public async Task GetCompanies_ReturnsMappedDtos()
        {
            // Arrange
            var companies = new List<Company>
            {
                new Company { CompanyId = 1, Name = "Test Company 1", Address = "123 Test St", Phone = "123-456-7890", Email = "test1@example.com" },
                new Company { CompanyId = 2, Name = "Test Company 2", Address = "456 Test St", Phone = "987-654-3210", Email = "test2@example.com" }
            };
            var companyDtos = new List<CompanyDto>
            {
                new CompanyDto { CompanyId = 1, Name = "Test Company 1", Address = "123 Test St", Phone = "123-456-7890", Email = "test1@example.com" },
                new CompanyDto { CompanyId = 2, Name = "Test Company 2", Address = "456 Test St", Phone = "987-654-3210", Email = "test2@example.com" }
            };

            _mockRepository.Setup(r => r.GetCompanies()).ReturnsAsync(companies);
            _mockMapper.Setup(m => m.Map<List<CompanyDto>>(companies)).Returns(companyDtos);

            // Act
            var result = await _service.GetCompanies();

            // Assert
            Assert.AreEqual(companyDtos, result);
        }

        [Test]
        public void GetCompanies_ThrowsKeyNotFoundException_WhenNoCompaniesFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetCompanies()).ReturnsAsync((IEnumerable<Company>)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetCompanies());
        }

        [Test]
        public async Task GetCompanyById_ReturnsMappedDto()
        {
            // Arrange
            var company = new Company { CompanyId = 1, Name = "Test Company", Address = "123 Test St", Phone = "123-456-7890", Email = "test@example.com" };
            var companyDto = new CompanyDto { CompanyId = 1, Name = "Test Company", Address = "123 Test St", Phone = "123-456-7890", Email = "test@example.com" };

            _mockRepository.Setup(r => r.GetCompanyById(1)).ReturnsAsync(company);
            _mockMapper.Setup(m => m.Map<CompanyDto>(company)).Returns(companyDto);

            // Act
            var result = await _service.GetCompanyById(1);

            // Assert
            Assert.AreEqual(companyDto, result);
        }

        [Test]
        public void GetCompanyById_ThrowsKeyNotFoundException_WhenCompanyNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetCompanyById(1)).ReturnsAsync((Company)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetCompanyById(1));
        }

        [Test]
        public async Task UpdateCompany_CallsRepositoryWithMappedEntity()
        {
            // Arrange
            var companyDto = new CompanyDto { CompanyId = 1, Name = "Updated Company", Address = "456 Updated St", Phone = "987-654-3210", Email = "updated@example.com" };
            var company = new Company { CompanyId = 1, Name = "Updated Company", Address = "456 Updated St", Phone = "987-654-3210", Email = "updated@example.com" };

            _mockMapper.Setup(m => m.Map<Company>(companyDto)).Returns(company);

            // Act
            await _service.UpdateCompany(companyDto);

            // Assert
            _mockRepository.Verify(r => r.UpdateCompany(company), Times.Once);
        }

        [Test]
        public void UpdateCompany_ThrowsKeyNotFoundException_WhenCompanyIdIsNull()
        {
            // Arrange
            var companyDto = new CompanyDto { CompanyId = 0, Name = "Updated Company", Address = "456 Updated St", Phone = "987-654-3210", Email = "updated@example.com" };

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.UpdateCompany(companyDto));
        }
    }
}
