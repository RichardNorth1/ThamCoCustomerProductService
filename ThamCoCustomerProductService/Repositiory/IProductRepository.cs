using ThamCoCustomerProductService.Data;

namespace ThamCoCustomerProductService.Repositiory
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProductsById(int productId);

        public Task<Product> UpdateProduct(Product product);

        public Task<Product> CreateProduct(Product product);

        public Task DeleteProduct(int productId);
    }
}
