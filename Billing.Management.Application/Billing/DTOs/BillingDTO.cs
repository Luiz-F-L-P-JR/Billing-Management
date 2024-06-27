
namespace Billing.Management.Application.Billing.DTOs
{
    public sealed record BillingDTO
    (
        Guid Id, 
        string? InvoiceNumber, 
        Customer.DTO.CustomerDTO? Customer, 
        DateTime? Date, 
        DateTime? DueDate, 
        string? TotalAmount, 
        string? Currency, 
        BillingLinesDTO? Lines
    );
}
