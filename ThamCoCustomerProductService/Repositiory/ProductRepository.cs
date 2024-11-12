using Microsoft.EntityFrameworkCore;
using ThamCoCustomerProductService.Data;

namespace ThamCoCustomerProductService.Repositiory
{
    public class ProductRepository : IProductRepository
    {

        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {

            var products = await _context.Products.ToListAsync();
            if (products == null)
            {
                throw new KeyNotFoundException();
            }
            return products;
        }

        public async Task<Product> GetProductsById(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var productOld = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (productOld == null)
            {
                throw new KeyNotFoundException();
            }

            productOld.Name = product.Name;
            productOld.Brand = product.Brand;
            productOld.Description = product.Description;
            productOld.ImageUrl = product.ImageUrl;

            _context.Update(productOld); // Update the retrieved customer object
            await _context.SaveChangesAsync();
            return productOld; // Return the updated customer
        }
    }
}
