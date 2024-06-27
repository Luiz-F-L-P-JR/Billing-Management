
using Billing.Management.Domain.Customer.Service.Interface;
using Billing.Management.Domain.Generic.Service;
using Billing.Management.Domain.UnitOfWork.Interface;

namespace Billing.Management.Domain.Customer.Service
{
    public sealed class CustomerDomainService : ServiceGeneric<Model.Customer>, ICustomerDomainService
    {
        public CustomerDomainService(IUnitOfWork<Model.Customer>? unitOfWork) 
            : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Model.Customer>> GetAllAsync()
        {
            return await _unitOfWork?.CustomerRepository.GetAllAsync();
        }
    }
}
