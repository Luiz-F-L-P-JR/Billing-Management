
using Billing.Management.Domain.Generic.Repository.Interface;

namespace Billing.Management.Domain.Billing.Repositories.Interfaces
{
    public interface IBillingRepository : IRepositoryGeneric<Models.Billing>
    {
        bool Exists(Guid id);
        Task<IEnumerable<Models.Billing>> GetAllAsync(int pagenumber, int pagesize);
        Task<IEnumerable<Models.Billing>> GetAllWithLinesAsync(int pagenumber, int pagesize);
    }
}
