
using Billing.Management.Domain.Billing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billing.Management.Infra.Data.Billing.FluentApis
{
    public class BillingLineFluentApi : IEntityTypeConfiguration<BillingLine>
    {
        public void Configure(EntityTypeBuilder<BillingLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new
            {
                x.Id,
                x.BillingId,
                x.ProductId
            });

            builder.Property(x => x.BillingId)
                   .IsRequired();

            builder.Property(x => x.ProductId)
                   .IsRequired();

            builder.Property(x => x.Quantity)
                   .IsRequired();

            builder.Property(x => x.UnitPrice)
                   .HasColumnType("REAL")
                   .HasPrecision(18, 6)
                   .IsRequired();

            builder.Property(x => x.Subtotal)
                   .HasColumnType("REAL")
                   .HasPrecision(18, 6)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(300)
                   .IsRequired();
        }
    }
}
