using System.ComponentModel.DataAnnotations;

namespace ThamCoCustomerProductService.Data
{
    public class Company
    {
        public Company()
        {
            
        }

        public Company(int companyId, string name, string address, string phone, string email)
        {
            CompanyId = companyId;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
        }

        [Key]
        public int CompanyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        public IEnumerable<CompanyProducts> CompanyProducts { get; set; }

    }
}
