
using Billing.Management.Application.Customer.DTO;
using System.Text.Json.Serialization;

namespace Billing.Management.Application.Billing.HttpRequests.DTOs
{
    public class BillingRequestDTO
    {
        [JsonPropertyName("invoice_number")]
        public string? InvoiceNumber { get; set; }

        public CustomerDTO? Customer { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;

        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal? TotalAmount { get; set; }
        public string? Currency { get; set; }
        public IList<BillingLineRequestDTO>? Lines { get; set; } = new List<BillingLineRequestDTO>();
    }
}
