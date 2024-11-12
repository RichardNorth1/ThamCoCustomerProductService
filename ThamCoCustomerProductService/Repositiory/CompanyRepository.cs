using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using ThamCoCustomerProductService.Data;

namespace ThamCoCustomerProductService.Repositiory
{
    public class CompanyRepository : ICompanyRepository
    {

        private readonly ProductDbContext _context;

        public CompanyRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task DeleteCompany(int CompanyId)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
            if (company == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var companies = await _context.Companies.ToListAsync();
            if (companies == null)
            {
                throw new KeyNotFoundException();
            }
            return companies;
        }

        public async Task<Company> GetCompanyById(int CompanyId)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
            if (company == null)
            {
                throw new KeyNotFoundException();
            }
            return company;
        }

        public async Task<Company> UpdateCompany(Company company)
        {
            var companyOld = await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == company.CompanyId);

            if (companyOld == null)
            {
                throw new KeyNotFoundException();
            }


            companyOld.Address = company.Address;
            companyOld.Phone = company.Phone;
            companyOld.Email = company.Email;
            companyOld.Name = company.Name;

            _context.Update(companyOld); // Update the retrieved customer object
            await _context.SaveChangesAsync();
            return companyOld; // Return the updated customer
        }
    }
}
