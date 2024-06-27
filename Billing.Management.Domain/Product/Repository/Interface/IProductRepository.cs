
using Billing.Management.Domain.Generic.Repository.Interface;

namespace Billing.Management.Domain.Product.Repository.Interface
{
    public interface IProductRepository : IRepositoryGeneric<Model.Product>
    {
        Task<IEnumerable<Model.Product>> GetAllAsync();
    }
}
