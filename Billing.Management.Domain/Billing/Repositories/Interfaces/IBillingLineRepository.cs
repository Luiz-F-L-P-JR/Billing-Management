
using Billing.Management.Domain.Billing.Models;

namespace Billing.Management.Domain.Billing.Repositories.Interfaces
{
    public interface IBillingLineRepository
    {
        bool Exists(Guid id);
        Task DeleteAsync(Guid id);
        Task CreateAsync(BillingLine entity);
    }
}
