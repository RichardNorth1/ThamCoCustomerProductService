using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThamCoCustomerProductService.Dtos;
using ThamCoCustomerProductService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThamCoCustomerProductService.Controllers
{
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
        [HttpGet("{companyId}/{productId}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyProduct(int companyId, int productId)
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
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompanyProduct(CompanyProductsDto companyProduct)
        {
            try
            {
                await _companyProductsService.CreateCompanyProduct(companyProduct);

                // Return 201 Created with the customer details
                return CreatedAtAction(nameof(GetCompanyProduct), new { id = companyProduct.CompanyId, companyProduct.ProductId }, companyProduct);
            }
            catch (Exception ex)
            {
                // Return 400 Bad Request with an error message
                return BadRequest("Failed to create the company product: " + ex.Message);
            }
        }

        // PUT api/CompanyProducts/5
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
