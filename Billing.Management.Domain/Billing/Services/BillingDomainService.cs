
using Billing.Management.Domain.Billing.Services.Interfaces;
using Billing.Management.Domain.Generic.Service;
using Billing.Management.Domain.UnitOfWork.Interface;

namespace Billing.Management.Domain.Billing.Services
{
    public sealed class BillingDomainService : ServiceGeneric<Models.Billing>, IBillingDomainService
    {
        public BillingDomainService(IUnitOfWork<Models.Billing>? unitOfWork) 
            : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Models.Billing>> GetAllAsync()
        {
            return await _unitOfWork?.BillingRepository.GetAllAsync();
        }
    }
}
