
namespace Billing.Management.Application.Billing.DTOs;

public sealed record BillingLineDTO(Guid Id, Guid BillingId, Guid ProductId, string? Description, int Quantity, decimal UnitPrice);
