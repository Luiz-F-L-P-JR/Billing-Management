
namespace Billing.Management.Application.Customer.DTO;

public sealed record CustomerDTO(Guid Id, string? Name, string? Email, string? Address);
