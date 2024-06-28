
using Billing.Management.Domain.Generic.Service;
using Billing.Management.Domain.Product.Service.Interface;
using Billing.Management.Domain.UnitOfWork.Interface;

namespace Billing.Management.Domain.Product.Service
{
    public sealed class ProductDomainService : ServiceGeneric<Model.Product>, IProductDomainService
    {
        public ProductDomainService(IUnitOfWork<Model.Product>? unitOfWork) 
            : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Model.Product>> GetAllAsync(int pagenumber, int pagesize)
        {
            return await _unitOfWork?.ProductRepository.GetAllAsync(pagenumber, pagesize);
        }
    }
}
