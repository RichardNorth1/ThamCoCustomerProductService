using System.ComponentModel.DataAnnotations;
using ThamCoCustomerProductService.Data;

namespace ThamCoCustomerProductService.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            
        }

        public ProductDto(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Brand = product.Brand;
            Description = product.Description;
            ImageUrl = product.ImageUrl;
        }

        public ProductDto(int productId, 
            string name,
            string brand, 
            string description, 
            string imageUrl)
        {
            ProductId = productId;
            Name = name;
            Brand = brand;
            Description = description;
            ImageUrl = imageUrl;
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
