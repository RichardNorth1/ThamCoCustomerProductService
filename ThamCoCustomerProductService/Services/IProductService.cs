using ThamCoCustomerProductService.Dtos;

namespace ThamCoCustomerProductService.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProducts();
        public Task<ProductDto> GetProductById(int productId);
        public Task<ProductDto> CreateProduct(ProductDto productDto);
        public Task UpdateProduct(ProductDto productDto);
        public Task DeleteProduct(int productId);
    }
}
