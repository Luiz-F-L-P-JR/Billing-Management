
using Billing.Management.Application.Customer.DTO;
using Billing.Management.Domain.Generic.Service.Interface;

namespace Billing.Management.Application.Customer.Service.Interface
{
    public interface ICustomerAppService : IServiceGeneric<CustomerDTO>
    {
        Task<IEnumerable<CustomerDTO>> GetAllAsync(int pagenumber, int pagesize);
    }
}
