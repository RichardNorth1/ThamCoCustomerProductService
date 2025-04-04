﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThamCoCustomerProductService.Dtos;
using ThamCoCustomerProductService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThamCoCustomerProductService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyProductsController : ControllerBase
    {
        private readonly ICompanyProductsService _companyProductsService;
        private readonly IMapper _mapper;

        public CompanyProductsController(ICompanyProductsService companyProductsService, IMapper mapper)
        {
            _companyProductsService = companyProductsService;
            _mapper = mapper;
        }

        // GET: api/CompanyProducts
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanyProducts()
        {
            try
            {
                var companyProducts = await _companyProductsService.GetCompanyProducts();
                return Ok(companyProducts);
            }
            catch (KeyNotFoundException x)
            {
                return NotFound(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/CompanyProducts/5
        [Authorize]
        [HttpGet("{companyId}/{productId}")]
        public async Task<ActionResult<CompanyProductsDto>> GetCompanyProduct(int companyId, int productId)
        {
            try
            {
                var companyProduct = await _companyProductsService.GetCompanyProductById(companyId, productId);
                return Ok(companyProduct);
            }
            catch (KeyNotFoundException x)
            {
                return NotFound(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST api/CompanyProducts
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CompanyProductsDto>> PostCompanyProduct(CompanyProductsDto companyProduct)
        {
            try
            {
                var createdCompanyProductDto = await _companyProductsService.CreateCompanyProduct(companyProduct);

                // Return 201 Created with the company product details
                return CreatedAtAction(nameof(GetCompanyProduct), new { companyId = createdCompanyProductDto.CompanyId, productId = createdCompanyProductDto.ProductId }, createdCompanyProductDto);
            }
            catch (Exception ex)
            {
                // Return 400 Bad Request with an error message
                return BadRequest("Failed to create the company product: " + ex.Message);
            }
        }


        // PUT api/CompanyProducts/5
        [Authorize]
        [HttpPut("{companyId}/{productId}")]
        public async Task<IActionResult> PutCompanyProduct(CompanyProductsDto companyProduct)
        {
            if (companyProduct == null)
            {
                return BadRequest();
            }

            try
            {
                await _companyProductsService.UpdateCompanyProduct(companyProduct);
            }
            catch (KeyNotFoundException x)
            {
                return NotFound(x);
            }
            catch (Exception)
            {
                return BadRequest();

            }

            return NoContent();
        }

        // DELETE api/CompanyProducts/5
        [Authorize]
        [HttpDelete("{companyId}/{productId}")]
        public async Task<IActionResult> DeleteCompany(int companyId, int productId)
        {
            try
            {
                await _companyProductsService.DeleteCompanyProduct(companyId, productId);
                return NoContent();
            }
            catch (KeyNotFoundException x)
            {
                return NotFound(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
