using AutoMapper;
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
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;

        }

        // GET: api/Products
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetProducts()
        {
            try
            {
                var products = await _productService.GetProducts();
                return Ok(products);
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

        // GET api/<ProductsController>/5
        [Authorize]
        [HttpGet("{productId}")]
        public async Task<ActionResult<CompanyDto>> GetProduct(int productId)
        {
            try
            {
                var product = await _productService.GetProductById(productId);
                return Ok(product);

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

        // POST api/<ProductsController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostProduct(ProductDto product)
        {
            try
            {
                await _productService.CreateProduct(product);

                // Return 201 Created with the customer details
                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                // Return 400 Bad Request with an error message
                return BadRequest("Failed to create the product: " + ex.Message);
            }
        }

        // PUT api/<ProductsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(ProductDto product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            try
            {
                await _productService.UpdateProduct(product);
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

        // DELETE api/<ProductsController>/5
        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteCompany(int productId)
        {
            try
            {
                await _productService.DeleteProduct(productId);
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
