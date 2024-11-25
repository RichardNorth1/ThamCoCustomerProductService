using System.ComponentModel.DataAnnotations;

namespace ThamCoCustomerProductService.Data
{
    public class CompanyProducts
    {

        public CompanyProducts()
        {
        }

        public CompanyProducts(int companyId, int productId, double price, int stockLevel)
        {
            CompanyId = companyId;
            ProductId = productId;
            Price = price;
            StockLevel = stockLevel;
        }

        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int StockLevel { get; set; }
        public Company Company { get; set; }
        public Product Product { get; set; }

    }
}
