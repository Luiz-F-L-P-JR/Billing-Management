
using Billing.Management.Domain.Generic.Service.Interface;

namespace Billing.Management.Domain.Product.Service.Interface
{
    public interface IProductDomainService : IServiceGeneric<Model.Product>
    {
        Task<IEnumerable<Model.Product>> GetAllAsync(int pagenumber, int pagesize);
    }
}
