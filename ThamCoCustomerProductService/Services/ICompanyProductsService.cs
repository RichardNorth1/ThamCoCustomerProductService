using ThamCoCustomerProductService.Dtos;

namespace ThamCoCustomerProductService.Services
{
    public interface ICompanyProductsService
    {
        public Task<IEnumerable<CompanyProductsDto>> GetCompanyProducts();
        public Task<CompanyProductsDto> GetCompanyProductById(int companyid, int productId);
        public Task<CompanyProductsDto> CreateCompanyProduct(CompanyProductsDto companyProductDto);
        public Task UpdateCompanyProduct(CompanyProductsDto companyProductDto);
        public Task DeleteCompanyProduct(int companyid, int productId);

    }
}
