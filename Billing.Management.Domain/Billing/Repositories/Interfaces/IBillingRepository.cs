
using Billing.Management.Domain.Generic.Repository.Interface;

namespace Billing.Management.Domain.Billing.Repositories.Interfaces
{
    public interface IBillingRepository : IRepositoryGeneric<Models.Billing>
    {
        Task<IEnumerable<Models.Billing>> GetAllAsync();
    }
}
