using AutoMapper;
using ThamCoCustomerProductService.Data;
using ThamCoCustomerProductService.Dtos;
using ThamCoCustomerProductService.Repositiory;

namespace ThamCoCustomerProductService.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.CreateProduct(product);
        }

        public async Task DeleteProduct(int productId)
        {
            if (productId <= 0)
            {
                throw new KeyNotFoundException();
            }
            await _productRepository.DeleteProduct(productId);
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _productRepository.GetProductsById(productId);

            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _productRepository.GetProducts();

            if (products == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task UpdateProduct(ProductDto productDto)
        {
            if (productDto.ProductId <= 0)
            {
                throw new KeyNotFoundException();
            }
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateProduct(product);
        }
    }
}
