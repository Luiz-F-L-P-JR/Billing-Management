
namespace Billing.Management.Domain.Billing.Models
{
    public sealed class Billing
    {
        public Guid Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public Customer.Model.Customer? Customer { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Currency { get; set; }
        public IList<BillingLine>? Lines { get; set; } = new List<BillingLine>();

        public Billing()
        {
            
        }

        public Billing(Guid id, string? invoiceNumber, Customer.Model.Customer? customer, DateTime? date, DateTime? dueDate, decimal? totalAmount, string? currency, IList<BillingLine>? lines)
        {
            Id = id;
            InvoiceNumber = invoiceNumber;
            Customer = customer;
            Date = date;
            DueDate = dueDate;
            TotalAmount = totalAmount;
            Currency = currency;
            Lines = lines;
        }
    }
}
