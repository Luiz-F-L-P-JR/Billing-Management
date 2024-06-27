
using Billing.Management.Domain.Billing.Models;
using Billing.Management.Domain.Generic.Repository.Interface;

namespace Billing.Management.Domain.Billing.Repositories.Interfaces
{
    public interface IBillingLineRepository : IRepositoryGeneric<BillingLine>
    {
        Task<IEnumerable<BillingLine>> GetAllAsync();
    }
}
