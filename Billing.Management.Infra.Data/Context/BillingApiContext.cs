
using Billing.Management.Domain.Billing.Models;
using Microsoft.EntityFrameworkCore;

namespace Billing.Management.Infra.Data.Context
{
    public class BillingApiContext : DbContext
    {
        public DbSet<BillingLine>? BillingLines { get; set; }
        public DbSet<Domain.Product.Model.Product>? Products { get; set; }
        public DbSet<Domain.Billing.Models.Billing>? Billings { get; set; }
        public DbSet<Domain.Customer.Model.Customer>? Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
