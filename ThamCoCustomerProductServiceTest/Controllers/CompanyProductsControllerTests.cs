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
    public class CompanyProductsControllerTests
    {
        private Mock<ICompanyProductsService> _mockCompanyProductsService;
        private Mock<IMapper> _mockMapper;
        private CompanyProductsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockCompanyProductsService = new Mock<ICompanyProductsService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CompanyProductsController(_mockCompanyProductsService.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetCompanyProducts_ReturnsOkResult_WithListOfCompanyProducts()
        {
            // Arrange
            var companyProducts = new List<CompanyProductsDto> { new CompanyProductsDto { CompanyId = 1, ProductId = 1 } };
            _mockCompanyProductsService.Setup(service => service.GetCompanyProducts()).ReturnsAsync(companyProducts);

            // Act
            var result = await _controller.GetCompanyProducts();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(companyProducts, okResult.Value);
        }

        [Test]
        public async Task GetCompanyProduct_ReturnsOkResult_WithCompanyProduct()
        {
            // Arrange
            var companyProduct = new CompanyProductsDto { CompanyId = 1, ProductId = 1 };
            _mockCompanyProductsService.Setup(service => service.GetCompanyProductById(1, 1)).ReturnsAsync(companyProduct);

            // Act
            var result = await _controller.GetCompanyProduct(1, 1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(companyProduct, okResult.Value);
        }

        [Test]
        public async Task PostCompanyProduct_ReturnsCreatedAtActionResult_WithCompanyProduct()
        {
            // Arrange
            var companyProduct = new CompanyProductsDto { CompanyId = 1, ProductId = 1 };
            _mockCompanyProductsService.Setup(service => service.CreateCompanyProduct(companyProduct)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostCompanyProduct(companyProduct);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(companyProduct, createdAtActionResult.Value);
        }

        [Test]
        public async Task DeleteCompany_ReturnsNoContentResult()
        {
            // Arrange
            _mockCompanyProductsService.Setup(service => service.DeleteCompanyProduct(1, 1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCompany(1, 1);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task GetCompanyProducts_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            // Arrange
            _mockCompanyProductsService.Setup(service => service.GetCompanyProducts()).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetCompanyProducts();

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task GetCompanyProduct_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            // Arrange
            _mockCompanyProductsService.Setup(service => service.GetCompanyProductById(1, 1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetCompanyProduct(1, 1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task PostCompanyProduct_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var companyProduct = new CompanyProductsDto { CompanyId = 1, ProductId = 1 };
            _mockCompanyProductsService.Setup(service => service.CreateCompanyProduct(companyProduct)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.PostCompanyProduct(companyProduct);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task DeleteCompany_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            // Arrange
            _mockCompanyProductsService.Setup(service => service.DeleteCompanyProduct(1, 1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.DeleteCompany(1, 1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task DeleteCompany_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            _mockCompanyProductsService.Setup(service => service.DeleteCompanyProduct(1, 1)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.DeleteCompany(1, 1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
