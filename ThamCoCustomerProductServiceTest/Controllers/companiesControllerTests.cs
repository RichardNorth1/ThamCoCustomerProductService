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
    public class CompaniesControllerTests
    {
        private Mock<ICompanyService> _mockCompanyService;
        private Mock<IMapper> _mockMapper;
        private CompaniesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockCompanyService = new Mock<ICompanyService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CompaniesController(_mockCompanyService.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetCompanies_ReturnsOkResult_WithListOfCompanies()
        {
            // Arrange
            var companies = new List<CompanyDto> { new CompanyDto { CompanyId = 1, Name = "Test Company" } };
            _mockCompanyService.Setup(service => service.GetCompanies()).ReturnsAsync(companies);

            // Act
            var result = await _controller.GetCompanies();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(companies, okResult.Value);
        }

        [Test]
        public async Task GetCompany_ReturnsOkResult_WithCompany()
        {
            // Arrange
            var company = new CompanyDto { CompanyId = 1, Name = "Test Company" };
            _mockCompanyService.Setup(service => service.GetCompanyById(1)).ReturnsAsync(company);

            // Act
            var result = await _controller.GetCompany(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(company, okResult.Value);
        }

        [Test]
        public async Task PostCompany_ReturnsCreatedAtActionResult_WithCompany()
        {
            // Arrange
            var company = new CompanyDto { CompanyId = 1, Name = "Test Company" };
            _mockCompanyService.Setup(service => service.CreateCompany(company)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostCompany(company);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(company, createdAtActionResult.Value);
        }

        [Test]
        public async Task PutCompany_ReturnsNoContentResult()
        {
            // Arrange
            var company = new CompanyDto { CompanyId = 1, Name = "Test Company" };
            _mockCompanyService.Setup(service => service.UpdateCompany(company)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCompany(company);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteCompany_ReturnsNoContentResult()
        {
            // Arrange
            _mockCompanyService.Setup(service => service.DeleteCompany(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCompany(1);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}


