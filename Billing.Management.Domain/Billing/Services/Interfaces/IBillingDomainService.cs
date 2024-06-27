
using Billing.Management.Domain.Generic.Service.Interface;

namespace Billing.Management.Domain.Billing.Services.Interfaces
{
    public interface IBillingDomainService : IServiceGeneric<Models.Billing>
    {
        Task<IEnumerable<Models.Billing>> GetAllAsync();
    }
}
