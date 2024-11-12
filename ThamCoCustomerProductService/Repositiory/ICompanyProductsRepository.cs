using ThamCoCustomerProductService.Data;

namespace ThamCoCustomerProductService.Repositiory
{
    public interface ICompanyProductsRepository
    {
        public Task<IEnumerable<CompanyProducts>> GetCompanyProducts();
        public Task<CompanyProducts> GetCompanyProductsById(int CompanyId, int productId);

        public Task<CompanyProducts> CreateCompanyProducts(CompanyProducts companyProducts);

        public Task<CompanyProducts> UpdateCompanyProducts(CompanyProducts companyProducts);

        public Task DeleteCompanyProducts(int CompanyId, int productId);
    }
}
