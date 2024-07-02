
using Billing.Management.Application.Billing.HttpRequests.DTOs;

namespace Billing.Management.Application.Billing.HttpRequests.Interface
{
    public interface IBillingServiceRequests
    {
        Task DeleteAsync(int id);
        Task<BillingRequestDTO> GetAsync(int id);
        Task CreateAsync(BillingRequestDTO entity);
        Task UpdateAsync(BillingRequestDTO entity);
        Task<IEnumerable<BillingRequestDTO>> GetAllAsync();
    }
}
