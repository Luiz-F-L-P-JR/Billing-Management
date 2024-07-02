
namespace Billing.Management.Domain.Customer.Model
{
    public sealed class Customer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public Customer()
        {
            
        }

        public Customer(Guid id, string? name, string? email, string? address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }
    }
}
