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
    public class CompanyProductsServiceTests
    {
        private Mock<ICompanyProductsRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private CompanyProductsService _service;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<ICompanyProductsRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CompanyProductsService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CreateCompanyProduct_CallsRepositoryWithMappedEntity()
        {
            // Arrange
            var companyProductDto = new CompanyProductsDto { CompanyId = 1, ProductId = 1 };
            var companyProduct = new CompanyProducts { CompanyId = 1, ProductId = 1 };

            _mockMapper.Setup(m => m.Map<CompanyProducts>(companyProductDto)).Returns(companyProduct);

            // Act
            await _service.CreateCompanyProduct(companyProductDto);

            // Assert
            _mockRepository.Verify(r => r.CreateCompanyProducts(companyProduct), Times.Once);
        }

        [Test]
        public void DeleteCompanyProduct_ThrowsKeyNotFoundException_WhenIdsAreNull()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.DeleteCompanyProduct(0, 0));
        }

        [Test]
        public async Task DeleteCompanyProduct_CallsRepositoryWithCorrectIds()
        {
            // Act
            await _service.DeleteCompanyProduct(1, 1);

            // Assert
            _mockRepository.Verify(r => r.DeleteCompanyProducts(1, 1), Times.Once);
        }

        [Test]
        public async Task GetCompanyProductById_ReturnsMappedDto()
        {
            // Arrange
            var companyProduct = new CompanyProducts { CompanyId = 1, ProductId = 1 };
            var companyProductDto = new CompanyProductsDto { CompanyId = 1, ProductId = 1 };

            _mockRepository.Setup(r => r.GetCompanyProductsById(1, 1)).ReturnsAsync(companyProduct);
            _mockMapper.Setup(m => m.Map<CompanyProductsDto>(companyProduct)).Returns(companyProductDto);

            // Act
            var result = await _service.GetCompanyProductById(1, 1);

            // Assert
            Assert.AreEqual(companyProductDto, result);
        }

        [Test]
        public void GetCompanyProductById_ThrowsKeyNotFoundException_WhenProductNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetCompanyProductsById(1, 1)).ReturnsAsync((CompanyProducts)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetCompanyProductById(1, 1));
        }

        [Test]
        public async Task GetCompanyProducts_ReturnsMappedDtos()
        {
            // Arrange
            var companyProducts = new List<CompanyProducts>
            {
                new CompanyProducts { CompanyId = 1, ProductId = 1 },
                new CompanyProducts { CompanyId = 2, ProductId = 2 }
            };
            var companyProductsDtos = new List<CompanyProductsDto>
            {
                new CompanyProductsDto { CompanyId = 1, ProductId = 1 },
                new CompanyProductsDto { CompanyId = 2, ProductId = 2 }
            };

            _mockRepository.Setup(r => r.GetCompanyProducts()).ReturnsAsync(companyProducts);
            _mockMapper.Setup(m => m.Map<List<CompanyProductsDto>>(companyProducts)).Returns(companyProductsDtos);

            // Act
            var result = await _service.GetCompanyProducts();

            // Assert
            Assert.AreEqual(companyProductsDtos, result);
        }

        [Test]
        public void GetCompanyProducts_ThrowsKeyNotFoundException_WhenNoProductsFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetCompanyProducts()).ReturnsAsync((IEnumerable<CompanyProducts>)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetCompanyProducts());
        }
    }
}
