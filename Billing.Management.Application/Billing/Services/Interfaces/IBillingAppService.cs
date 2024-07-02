
using Billing.Management.Application.Billing.DTOs;
using Billing.Management.Domain.Generic.Service.Interface;

namespace Billing.Management.Application.Billing.Services.Interfaces
{
    public interface IBillingAppService : IServiceGeneric<BillingDTO>
    {
        Task CreateFromRequestAsync();
        Task<IEnumerable<BillingDTO>> GetAllAsync(int pagenumber, int pagesize);
        Task<IEnumerable<BillingDTO>> GetAllWithLinesAsync(int pagenumber, int pagesize);
    }
}
