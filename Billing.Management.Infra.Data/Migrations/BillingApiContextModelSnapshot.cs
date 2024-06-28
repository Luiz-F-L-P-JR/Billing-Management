
using Billing.Management.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace Billing.Management.Infra.Data.Migrations
{
    [DbContext(typeof(BillingApiContext))]
    partial class BillingApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Billing.Management.Domain.Billing.Models.Billing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DueDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("TotalAmount")
                        .IsRequired()
                        .HasPrecision(18, 6)
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("Id", "InvoiceNumber");

                    b.ToTable("Billings");
                });

            modelBuilder.Entity("Billing.Management.Domain.Billing.Models.BillingLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BillingId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Subtotal")
                        .HasPrecision(18, 6)
                        .HasColumnType("REAL");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 6)
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("BillingId");

                    b.HasIndex("Id", "BillingId", "ProductId");

                    b.ToTable("BillingLines");
                });

            modelBuilder.Entity("Billing.Management.Domain.Customer.Model.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Id", "Name");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12081264-5645-407a-ae37-78d5da96fe59"),
                            Address = "Rua Exemplo 1, 123",
                            Email = "cliente1@example.com",
                            Name = "Cliente Exemplo 1"
                        });
                });

            modelBuilder.Entity("Billing.Management.Domain.Product.Model.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Id", "Name");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a683"),
                            Name = "Produto 1"
                        },
                        new
                        {
                            Id = new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a612"),
                            Name = "Produto 2"
                        });
                });

            modelBuilder.Entity("Billing.Management.Domain.Billing.Models.Billing", b =>
                {
                    b.HasOne("Billing.Management.Domain.Customer.Model.Customer", "Customer")
                        .WithOne()
                        .HasForeignKey("Billing.Management.Domain.Billing.Models.Billing", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Billing.Management.Domain.Billing.Models.BillingLine", b =>
                {
                    b.HasOne("Billing.Management.Domain.Billing.Models.Billing", null)
                        .WithMany("Lines")
                        .HasForeignKey("BillingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Billing.Management.Domain.Billing.Models.Billing", b =>
                {
                    b.Navigation("Lines");
                });
#pragma warning restore 612, 618
        }
    }
}
