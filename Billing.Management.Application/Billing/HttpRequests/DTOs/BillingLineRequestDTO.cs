
using System.Text.Json.Serialization;

namespace Billing.Management.Application.Billing.HttpRequests.DTOs
{
    public sealed class BillingLineRequestDTO
    {
        public Guid ProductId { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }

        public decimal Subtotal { get; set; }
    }
}
