using System.ComponentModel.DataAnnotations;

namespace ThamCoCustomerProductService.Data
{
    public class Product
    {
        public Product()
        {
            
        }
        public Product(int productId, 
            string name, 
            string brand, 
            string description, 
            double price, 
            string imageUrl)
        {
            ProductId = productId;
            Name = name;
            Brand = brand;
            Description = description;
            ImageUrl = imageUrl;
        }

        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public List<CompanyProducts> CompanyProducts { get; set; }
    }
}
