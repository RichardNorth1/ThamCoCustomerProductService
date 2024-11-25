using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using ThamCoCustomerProductService.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ThamCoCustomerProductService.Repositiory
{
    public class CompanyProductsRepository : ICompanyProductsRepository
    {
        private readonly ProductDbContext _context;

        public CompanyProductsRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyProducts> CreateCompanyProducts(CompanyProducts companyProducts)
        {
            _context.CompanyProducts.Add(companyProducts);
            await _context.SaveChangesAsync();
            return companyProducts;
        }

        public async Task DeleteCompanyProducts(int CompanyId, int productId)
        {
            var companyProduct = await _context.CompanyProducts.FirstOrDefaultAsync(cp => cp.ProductId == productId && cp.CompanyId == CompanyId);
            if (companyProduct == null)
            {
                throw new KeyNotFoundException();
            }

            _context.CompanyProducts.Remove(companyProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompanyProducts>> GetCompanyProducts()
        {
            var companyProducts = await _context.CompanyProducts.ToListAsync();
            if (companyProducts == null)
            {
                throw new KeyNotFoundException();
            }
            return companyProducts;
        }

        public async Task<CompanyProducts> GetCompanyProductsById(int CompanyId, int productId)
        {
            var companyProducts = await _context.CompanyProducts.FirstOrDefaultAsync(cp => cp.ProductId == productId && cp.CompanyId == CompanyId);
            if (companyProducts == null)
            {
                throw new KeyNotFoundException();
            }
            return companyProducts;
        }

        public async Task<CompanyProducts> UpdateCompanyProducts(CompanyProducts companyProduct)
        {
            var companyProductOld = await _context.CompanyProducts
                .FirstOrDefaultAsync(cp => cp.ProductId == companyProduct.ProductId && cp.CompanyId == companyProduct.CompanyId);


            if (companyProduct == null)
            {
                throw new KeyNotFoundException();
            }


            companyProductOld.Price = companyProduct.Price;
            companyProductOld.StockLevel = companyProduct.StockLevel;


            _context.Update(companyProductOld);
            await _context.SaveChangesAsync();
            return companyProductOld;
        }
    }
}
