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
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private ProductService _service;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ProductService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CreateProduct_CallsRepositoryWithMappedEntity()
        {
            // Arrange
            var productDto = new ProductDto { ProductId = 1, Name = "Test Product", Brand = "Test Brand", Description = "Test Description", ImageUrl = "http://example.com/image.jpg" };
            var product = new Product { ProductId = 1, Name = "Test Product", Brand = "Test Brand", Description = "Test Description", ImageUrl = "http://example.com/image.jpg" };

            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);

            // Act
            await _service.CreateProduct(productDto);

            // Assert
            _mockRepository.Verify(r => r.CreateProduct(product), Times.Once);
        }

        [Test]
        public void DeleteProduct_ThrowsKeyNotFoundException_WhenProductIdIsNull()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.DeleteProduct(0));
        }

        [Test]
        public async Task DeleteProduct_CallsRepositoryWithCorrectId()
        {
            // Act
            await _service.DeleteProduct(1);

            // Assert
            _mockRepository.Verify(r => r.DeleteProduct(1), Times.Once);
        }

        [Test]
        public async Task GetProductById_ReturnsMappedDto()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Test Product", Brand = "Test Brand", Description = "Test Description", ImageUrl = "http://example.com/image.jpg" };
            var productDto = new ProductDto { ProductId = 1, Name = "Test Product", Brand = "Test Brand", Description = "Test Description", ImageUrl = "http://example.com/image.jpg" };

            _mockRepository.Setup(r => r.GetProductsById(1)).ReturnsAsync(product);
            _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

            // Act
            var result = await _service.GetProductById(1);

            // Assert
            Assert.AreEqual(productDto, result);
        }

        [Test]
        public void GetProductById_ThrowsKeyNotFoundException_WhenProductNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetProductsById(1)).ReturnsAsync((Product)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetProductById(1));
        }

        [Test]
        public async Task GetProducts_ReturnsMappedDtos()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Test Product 1", Brand = "Test Brand 1", Description = "Test Description 1", ImageUrl = "http://example.com/image1.jpg" },
                new Product { ProductId = 2, Name = "Test Product 2", Brand = "Test Brand 2", Description = "Test Description 2", ImageUrl = "http://example.com/image2.jpg" }
            };
            var productDtos = new List<ProductDto>
            {
                new ProductDto { ProductId = 1, Name = "Test Product 1", Brand = "Test Brand 1", Description = "Test Description 1", ImageUrl = "http://example.com/image1.jpg" },
                new ProductDto { ProductId = 2, Name = "Test Product 2", Brand = "Test Brand 2", Description = "Test Description 2", ImageUrl = "http://example.com/image2.jpg" }
            };

            _mockRepository.Setup(r => r.GetProducts()).ReturnsAsync(products);
            _mockMapper.Setup(m => m.Map<List<ProductDto>>(products)).Returns(productDtos);

            // Act
            var result = await _service.GetProducts();

            // Assert
            Assert.AreEqual(productDtos, result);
        }

        [Test]
        public void GetProducts_ThrowsKeyNotFoundException_WhenNoProductsFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetProducts()).ReturnsAsync((IEnumerable<Product>)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetProducts());
        }

        [Test]
        public async Task UpdateProduct_CallsRepositoryWithMappedEntity()
        {
            // Arrange
            var productDto = new ProductDto { ProductId = 1, Name = "Updated Product", Brand = "Updated Brand", Description = "Updated Description", ImageUrl = "http://example.com/updated_image.jpg" };
            var product = new Product { ProductId = 1, Name = "Updated Product", Brand = "Updated Brand", Description = "Updated Description", ImageUrl = "http://example.com/updated_image.jpg" };

            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);

            // Act
            await _service.UpdateProduct(productDto);

            // Assert
            _mockRepository.Verify(r => r.UpdateProduct(product), Times.Once);
        }

        [Test]
        public void UpdateProduct_ThrowsKeyNotFoundException_WhenProductIdIsNull()
        {
            // Arrange
            var productDto = new ProductDto { ProductId = 0, Name = "Updated Product", Brand = "Updated Brand", Description = "Updated Description", ImageUrl = "http://example.com/updated_image.jpg" };

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.UpdateProduct(productDto));
        }
    }
}
