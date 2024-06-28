
using Billing.Management.Application.Product.DTO;
using Billing.Management.Domain.Generic.Service.Interface;

namespace Billing.Management.Application.Product.Service.Interface
{
    public interface IProductAppService : IServiceGeneric<ProductDTO>
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync(int pagenumber, int pagesize);
    }
}
