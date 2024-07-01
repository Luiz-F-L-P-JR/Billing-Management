
using Billing.Management.Domain.Generic.Repository.Interface;

namespace Billing.Management.Domain.Product.Repository.Interface
{
    public interface IProductRepository : IRepositoryGeneric<Model.Product>
    {
        bool Exists(Guid id);
        Task<IEnumerable<Model.Product>> GetAllAsync(int pagenumber, int pagesize);
    }
}
