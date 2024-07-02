
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billing.Management.Infra.Data.Product.FluentApi
{
    public class ProductFluentApi : IEntityTypeConfiguration<Domain.Product.Model.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Product.Model.Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new
            {
                x.Id,
                x.Name
            });

            builder.Property(x => x.Name)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
