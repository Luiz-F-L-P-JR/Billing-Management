
using Billing.Management.Domain.Generic.Repository.Interface;

namespace Billing.Management.Domain.Customer.Repository.Interface
{
    public interface ICustomerRepository : IRepositoryGeneric<Model.Customer>
    {
        bool Exists(Guid id);
        Task<IEnumerable<Model.Customer>> GetAllAsync(int pagenumber, int pagesize);
    }
}
