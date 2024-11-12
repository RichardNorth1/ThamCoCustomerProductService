using System.ComponentModel.DataAnnotations;
using ThamCoCustomerProductService.Data;

namespace ThamCoCustomerProductService.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
            
        }
        public CompanyDto(Company company)
        {
            CompanyId = company.CompanyId;
            Name = company.Name;
            Address = company.Address;
            Phone = company.Phone;
            Email = company.Email;
        }

        public CompanyDto(int companyId, string name, string address, string phone, string email)
        {
            CompanyId = companyId;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
