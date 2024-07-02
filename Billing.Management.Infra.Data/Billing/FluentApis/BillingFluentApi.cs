
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billing.Management.Infra.Data.Billing.FluentApis
{
    public class BillingFluentApi : IEntityTypeConfiguration<Domain.Billing.Models.Billing>
    {
        public void Configure(EntityTypeBuilder<Domain.Billing.Models.Billing> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new
            {
                x.Id,
                x.CustomerId,
                x.InvoiceNumber
            });

            builder.Property(x => x.CustomerId)
                   .IsRequired();

            builder.Property(x => x.Date)
                   .IsRequired();

            builder.Property(x => x.DueDate)
                   .IsRequired();

            builder.Property(x => x.Currency)
                   .IsRequired()
                   .HasMaxLength(5);

            builder.Property(x => x.TotalAmount)
                   .HasColumnType("REAL")
                   .HasPrecision(18, 6)
                   .IsRequired();

            builder.HasMany(x => x.Lines)
                   .WithOne()
                   .HasForeignKey(x => x.BillingId);
        }
    }
}
