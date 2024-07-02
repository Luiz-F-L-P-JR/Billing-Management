
using Billing.Management.Domain.Generic.Service.Interface;

namespace Billing.Management.Domain.Customer.Service.Interface
{
    public interface ICustomerDomainService : IServiceGeneric<Model.Customer>
    {
        Task<IEnumerable<Model.Customer>> GetAllAsync(int pagenumber, int pagesize);
    }
}
