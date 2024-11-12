using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ThamCoCustomerProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {

        }

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
    : base(options)
        {

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyProducts> CompanyProducts { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyProducts>()
                .HasKey(a => new { a.ProductId, a.CompanyId });

            modelBuilder.Entity<CompanyProducts>()
                .HasOne(p => p.Product)
                .WithMany(cp => cp.CompanyProducts)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<CompanyProducts>()
                .HasOne(c => c.Company)
                .WithMany(cp => cp.CompanyProducts)
                .HasForeignKey(m => m.CompanyId);

        }
    }
}
