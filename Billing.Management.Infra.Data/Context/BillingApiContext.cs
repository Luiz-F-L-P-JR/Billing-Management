
using Billing.Management.Domain.Billing.Models;
using Billing.Management.Infra.Data.Billing.FluentApis;
using Billing.Management.Infra.Data.Customer.FluentApi;
using Billing.Management.Infra.Data.Product.FluentApi;
using Microsoft.EntityFrameworkCore;

namespace Billing.Management.Infra.Data.Context
{
    public class BillingApiContext : DbContext
    {
        public DbSet<BillingLine>? BillingLines { get; set; }
        public DbSet<Domain.Product.Model.Product>? Products { get; set; }
        public DbSet<Domain.Billing.Models.Billing>? Billings { get; set; }
        public DbSet<Domain.Customer.Model.Customer>? Customers { get; set; }

        public BillingApiContext(DbContextOptions<BillingApiContext> options)
            : base(options)
        {
                        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BillingFluentApi());
            modelBuilder.ApplyConfiguration(new ProductFluentApi());
            modelBuilder.ApplyConfiguration(new CustomerFluentApi());
            modelBuilder.ApplyConfiguration(new BillingLineFluentApi());

            modelBuilder.Entity<Domain.Customer.Model.Customer>()
            .HasData(
                new Domain.Customer.Model.Customer
                {
                    Id = Guid.Parse("12081264-5645-407a-ae37-78d5da96fe59"),
                    Name = "Cliente Exemplo 1",
                    Email = "cliente1@example.com",
                    Address = "Rua Exemplo 1, 123"
                }
            );

            modelBuilder.Entity<Domain.Product.Model.Product>()
            .HasData(
                new List<Domain.Product.Model.Product>()
                {
                    new Domain.Product.Model.Product
                    {
                        Id = Guid.Parse("48c6dc20-a943-4f8f-83ca-1e1cf094a683"),
                        Name = "Produto 1"
                    },

                    new Domain.Product.Model.Product
                    {
                        Id = Guid.Parse("48c6dc20-a943-4f8f-83ca-1e1cf094a612"),
                        Name = "Produto 2"
                    }
                }
            );
        }
    }
}
