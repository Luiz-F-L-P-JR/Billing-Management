
namespace Billing.Management.Application.Billing.DTOs;

public sealed record BillingLinesDTO(Guid BillingId, Guid ProductId, string? Description, int Quantity, decimal UnitPrice);
