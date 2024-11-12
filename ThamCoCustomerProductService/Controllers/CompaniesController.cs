using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThamCoCustomerProductService.Dtos;
using ThamCoCustomerProductService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThamCoCustomerProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            try
            {
                var companies = await _companyService.GetCompanies();
                return Ok(companies);
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

        // GET api/Companies/5
        [HttpGet("{companyId}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(int companyId)
        {
            try
            {
                var company = await _companyService.GetCompanyById(companyId);
                return Ok(company);

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

        // POST api/Companies
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompany(CompanyDto company)
        {
            try
            {
                await _companyService.CreateCompany(company);

                // Return 201 Created with the customer details
                return CreatedAtAction(nameof(GetCompany), new { id = company.CompanyId }, company);
            }
            catch (Exception ex)
            {
                // Return 400 Bad Request with an error message
                return BadRequest("Failed to create the company: " + ex.Message);
            }
        }

        // PUT api/Companies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(CompanyDto company)
        {
            if (company == null)
            {
                return BadRequest();
            }

            try
            {
                await _companyService.UpdateCompany(company);
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

        // DELETE api/Companies/5
        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            try
            {
                await _companyService.DeleteCompany(companyId);
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
