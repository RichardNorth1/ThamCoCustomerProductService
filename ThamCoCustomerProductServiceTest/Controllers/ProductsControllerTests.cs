using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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
            var products = new List<ProductDto> { 
                new ProductDto {
                    ProductId = 1,
                    Name = "Test Name",
                    Brand = "Test Brand",
                    Description = "Test Description",
                    ImageUrl = "Test Image url"
                } 
            };
            _mockProductService.Setup(service => service.GetProducts()).ReturnsAsync(products);

            var result = await _controller.GetProducts();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(products, okResult.Value);
        }

        [Test]
        public async Task GetProduct_ReturnsOkResult_WithProduct()
        {
            var product = new ProductDto
            {
                ProductId = 1,
                Name = "Test Name",
                Brand = "Test Brand",
                Description = "Test Description",
                ImageUrl = "Test Image url"
            };
            _mockProductService.Setup(service => service.GetProductById(1)).ReturnsAsync(product);

            var result = await _controller.GetProduct(1);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(product, okResult.Value);
        }
        [Test]
        public async Task PostProduct_ReturnsCreatedAtActionResult_WithProduct()
        {
            var product = new ProductDto
            {
                ProductId = 1,
                Name = "Test Name",
                Brand = "Test Brand",
                Description = "Test Description",
                ImageUrl = "Test Image url"
            };
            _mockProductService.Setup(service => service.CreateProduct(product)).ReturnsAsync(product);

            var result = await _controller.PostProduct(product);

            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(product, createdAtActionResult.Value);
        }


        [Test]
        public async Task PutProduct_ReturnsNoContentResult()
        {
            var product = new ProductDto
            {
                ProductId = 1,
                Name = "Test Name",
                Brand = "Test Brand",
                Description = "Test Description",
                ImageUrl = "Test Image url"
            };
            _mockProductService.Setup(service => service.UpdateProduct(product)).Returns(Task.CompletedTask);

            var result = await _controller.PutProduct(product);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteProduct_ReturnsNoContentResult()
        {
            _mockProductService.Setup(service => service.DeleteProduct(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteCompany(1);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task GetProducts_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            _mockProductService.Setup(service => service.GetProducts()).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.GetProducts();

            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task GetProduct_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            _mockProductService.Setup(service => service.GetProductById(1)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.GetProduct(1);

            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task PostProduct_ReturnsBadRequest_WhenExceptionThrown()
        {
            var product = new ProductDto
            {
                ProductId = 1,
                Name = "Test Name",
                Brand = "Test Brand",
                Description = "Test Description",
                ImageUrl = "Test Image url"
            };
            _mockProductService.Setup(service => service.CreateProduct(product)).ThrowsAsync(new Exception("Error"));

            var result = await _controller.PostProduct(product);

            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task PutProduct_ReturnsBadRequest_WhenExceptionThrown()
        {
            var product = new ProductDto
            {
                ProductId = 1,
                Name = "Test Name",
                Brand = "Test Brand",
                Description = "Test Description",
                ImageUrl = "Test Image url"
            };
            _mockProductService.Setup(service => service.UpdateProduct(product)).ThrowsAsync(new Exception("Error"));

            var result = await _controller.PutProduct(product);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task DeleteProduct_ReturnsNotFound_WhenKeyNotFoundExceptionThrown()
        {
            _mockProductService.Setup(service => service.DeleteProduct(1)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.DeleteCompany(1);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task DeleteProduct_ReturnsBadRequest_WhenExceptionThrown()
        {
            _mockProductService.Setup(service => service.DeleteProduct(1)).ThrowsAsync(new Exception("Error"));

            var result = await _controller.DeleteCompany(1);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
