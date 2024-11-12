using ThamCoCustomerProductService.Data;

namespace ThamCoCustomerProductService.Repositiory
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompanyById(int CompanyId);

        public Task<Company> UpdateCompany(Company company);

        public Task<Company> CreateCompany(Company company);

        public Task DeleteCompany(int CompanyId);
    }
}
