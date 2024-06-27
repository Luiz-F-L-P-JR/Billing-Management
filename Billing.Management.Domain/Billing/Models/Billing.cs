
using Billing.Management.Domain;

namespace Billing.Management.Domain.Billing.Models
{
    public sealed class Billing
    {
        public Guid Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public Customer.Model.Customer? Customer { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public string? TotalAmount { get; set; }
        public string? Currency { get; set; }
        public BillingLine? Lines { get; set; }

        public Billing()
        {
            
        }

        public Billing(Guid id, string? invoiceNumber, Customer.Model.Customer? customer, DateTime? date, DateTime? dueDate, string? totalAmount, string? currency, BillingLine? lines)
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
