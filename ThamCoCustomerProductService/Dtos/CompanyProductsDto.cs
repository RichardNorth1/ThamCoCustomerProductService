using System.ComponentModel.Design;
using ThamCoCustomerProductService.Data;

namespace ThamCoCustomerProductService.Dtos
{
    public class CompanyProductsDto
    {
        public CompanyProductsDto()
        {
            
        }

        public CompanyProductsDto(CompanyProducts companyProducts)
        {
            CompanyId = companyProducts.CompanyId;
            ProductId = companyProducts.ProductId;
            Price = companyProducts.Price;
            StockLevel = companyProducts.StockLevel;
        }

        public CompanyProductsDto(int companyId, int productId, double price, int stockLevel)
        {
            CompanyId = companyId;
            ProductId = productId;
            Price = price;
            StockLevel = stockLevel;
        }

        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int StockLevel { get; set; }


    }
}
