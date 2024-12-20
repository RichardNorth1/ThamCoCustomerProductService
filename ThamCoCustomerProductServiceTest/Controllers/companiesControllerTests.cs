using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
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
            var companies = new List<CompanyDto> {
                new CompanyDto {
                    CompanyId = 1,
                    Name = "Test name",
                    Address = "Test Address",
                    Phone = "01642 666666",
                    Email = "Test@email,com"
                } 
            };
            _mockCompanyService.Setup(service => service.GetCompanies()).ReturnsAsync(companies);

            var result = await _controller.GetCompanies();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(companies, okResult.Value);
        }

        [Test]
        public async Task GetCompany_ReturnsOkResult_WithCompany()
        {
            var company = new CompanyDto
            {
                CompanyId = 1,
                Name = "Test name",
                Address = "Test Address",
                Phone = "01642 666666",
                Email = "Test@email,com"
            };
            _mockCompanyService.Setup(service => service.GetCompanyById(1)).ReturnsAsync(company);

            var result = await _controller.GetCompany(1);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(company, okResult.Value);
        }

        [Test]
        public async Task PostCompany_ReturnsCreatedAtActionResult_WithCompany()
        {
            var company = new CompanyDto
            {
                CompanyId = 1,
                Name = "Test name",
                Address = "Test Address",
                Phone = "01642 666666",
                Email = "Test@email.com"
            };
            _mockCompanyService.Setup(service => service.CreateCompany(company)).ReturnsAsync(company);

            var result = await _controller.PostCompany(company);

            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(company, createdAtActionResult.Value);
        }


        [Test]
        public async Task PutCompany_ReturnsNoContentResult()
        {
            var company = new CompanyDto
            {
                CompanyId = 1,
                Name = "Test name",
                Address = "Test Address",
                Phone = "01642 666666",
                Email = "Test@email,com"
            };
            _mockCompanyService.Setup(service => service.UpdateCompany(company)).Returns(Task.CompletedTask);

            var result = await _controller.PutCompany(company);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteCompany_ReturnsNoContentResult()
        {
            _mockCompanyService.Setup(service => service.DeleteCompany(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteCompany(1);

            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}


