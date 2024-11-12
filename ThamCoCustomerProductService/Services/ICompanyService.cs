using ThamCoCustomerProductService.Dtos;

namespace ThamCoCustomerProductService.Services
{
    public interface ICompanyService
    {
        public Task<IEnumerable<CompanyDto>> GetCompanies();
        public Task<CompanyDto> GetCompanyById(int companyid);
        public Task CreateCompany(CompanyDto companyDto);
        public Task UpdateCompany(CompanyDto companyDto);
        public Task DeleteCompany(int companyid);
    }
}
