using AutoMapper;
using ThamCoCustomerProductService.Data;
using ThamCoCustomerProductService.Dtos;
using ThamCoCustomerProductService.Repositiory;

namespace ThamCoCustomerProductService.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;   
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDto> CreateCompany(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            var companyData = await _companyRepository.CreateCompany(company);
            companyDto = _mapper.Map<CompanyDto>(companyData);
            return companyDto;
        }

        public async Task DeleteCompany(int companyid)
        {
            if (companyid <= 0)
            {
                throw new KeyNotFoundException();
            }
            await _companyRepository.DeleteCompany(companyid);
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanies()
        {
            var companies = await _companyRepository.GetCompanies();

            if (companies == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<List<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> GetCompanyById(int companyid)
        {
            var company = await _companyRepository.GetCompanyById(companyid);

            if (company == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<CompanyDto>(company);
        }

        public async Task UpdateCompany(CompanyDto companyDto)
        {
            if (companyDto.CompanyId <= 0)
            {
                throw new KeyNotFoundException();
            }
            var company = _mapper.Map<Company>(companyDto);
            await _companyRepository.UpdateCompany(company);
        }
    }
}
