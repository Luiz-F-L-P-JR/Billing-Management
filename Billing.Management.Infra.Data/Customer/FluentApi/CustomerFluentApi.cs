
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billing.Management.Infra.Data.Customer.FluentApi
{
    public class CustomerFluentApi : IEntityTypeConfiguration<Domain.Customer.Model.Customer>
    {
        public void Configure(EntityTypeBuilder<Domain.Customer.Model.Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new
            {
                x.Id,
                x.Name
            });

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Address)
                   .HasMaxLength(300)
                   .IsRequired();
        }
    }
}
