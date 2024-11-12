using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThamCoCustomerProductService.Controllers;
using ThamCoCustomerProductService.Dtos;
using ThamCoCustomerProductService.Services;

namespace ThamCoCustomerProductService.Tests.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<IProductService> _mockProductService;
        private Mock<IMapper> _mockMapper;
        private ProductsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockProductService = new Mock<IProductService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ProductsController(_mockProductService.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<ProductDto> { new ProductDto { ProductId = 1, Name = "Test Product" } };
            _mockProductService.Setup(service => service.GetProducts()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(products, okResult.Value);
        }

        [Test]
        public async Task GetProduct_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new ProductDto { ProductId = 1, Name = "Test Product" };
            _mockProductService.Setup(service => service.GetProductById(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(product, okResult.Value);
        }

        [Test]
        public async Task PostProduct_ReturnsCreatedAtActionResult_WithProduct()
        {
            // Arrange
            var product = new ProductDto { ProductId = 1, Name = "Test Product" };
            _mockProductService.Setup(service => service.CreateProduct(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostProduct(product);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(product, createdAtActionResult.Value);
        }

        [Test]
        public async Task PutProduct_ReturnsNoContentResult()
        {
            // Arrange
            var product = new ProductDto { ProductId = 1, Name = "Test Product" };
            _mockProductService.Setup(service => service.UpdateProduct(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutProduct(product);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteProduct_ReturnsNoContentResult()
        {
            // Arrange
            _mockProductService.Setup(service => service.DeleteProduct(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCompany(1);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task GetProducts_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            // Arrange
            _mockProductService.Setup(service => service.GetProducts()).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetProducts();

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task GetProduct_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            // Arrange
            _mockProductService.Setup(service => service.GetProductById(1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task PostProduct_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var product = new ProductDto { ProductId = 1, Name = "Test Product" };
            _mockProductService.Setup(service => service.CreateProduct(product)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.PostProduct(product);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task PutProduct_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var product = new ProductDto { ProductId = 1, Name = "Test Product" };
            _mockProductService.Setup(service => service.UpdateProduct(product)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.PutProduct(product);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task DeleteProduct_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            // Arrange
            _mockProductService.Setup(service => service.DeleteProduct(1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.DeleteCompany(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task DeleteProduct_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            _mockProductService.Setup(service => service.DeleteProduct(1)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.DeleteCompany(1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
