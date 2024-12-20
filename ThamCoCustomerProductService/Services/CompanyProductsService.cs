using AutoMapper;
using ThamCoCustomerProductService.Data;
using ThamCoCustomerProductService.Dtos;
using ThamCoCustomerProductService.Repositiory;

namespace ThamCoCustomerProductService.Services
{
    public class CompanyProductsService : ICompanyProductsService
    {
        private readonly ICompanyProductsRepository _companyProductsRepository;
        private readonly IMapper _mapper;

        public CompanyProductsService(ICompanyProductsRepository companyProductsRepository, IMapper mapper)
        {
            _companyProductsRepository = companyProductsRepository;
            _mapper = mapper;
        }
        public async Task<CompanyProductsDto> CreateCompanyProduct(CompanyProductsDto companyProducDto)
        {
            var companyProduct = _mapper.Map<CompanyProducts>(companyProducDto);
            var companyProductData = await _companyProductsRepository.CreateCompanyProducts(companyProduct);
            companyProducDto = _mapper.Map<CompanyProductsDto>(companyProductData);
            return companyProducDto;
        }

        public async Task DeleteCompanyProduct(int companyid, int productId)
        {
            if (companyid <= 0 || productId <= 0)
            {
                throw new KeyNotFoundException("Invalid company or product ID.");
            }
            await _companyProductsRepository.DeleteCompanyProducts(companyid, productId);
        }

        public async Task<CompanyProductsDto> GetCompanyProductById(int companyid, int productId)
        {
            var companyProduct = await _companyProductsRepository.GetCompanyProductsById(companyid, productId);

            if (companyProduct == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<CompanyProductsDto>(companyProduct);
        }

        public async Task<IEnumerable<CompanyProductsDto>> GetCompanyProducts()
        {
            var companyProducts = await _companyProductsRepository.GetCompanyProducts();

            if (companyProducts == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<List<CompanyProductsDto>>(companyProducts);
        }

        public async Task UpdateCompanyProduct(CompanyProductsDto companyProductDto)
        {
            if (companyProductDto.ProductId <= 0 || companyProductDto.CompanyId <= 0)
            {
                throw new KeyNotFoundException();
            }
            var companyProduct = _mapper.Map<CompanyProducts>(companyProductDto);
            await _companyProductsRepository.UpdateCompanyProducts(companyProduct);
        }
    }
}
