
namespace Billing.Management.Domain.Product.Model
{
    public sealed class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public Product()
        {
            
        }

        public Product(Guid id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
