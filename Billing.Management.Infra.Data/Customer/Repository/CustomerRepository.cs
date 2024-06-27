
using Billing.Management.Domain.Customer.Repository.Interface;
using Billing.Management.Infra.Data.Context;
using Billing.Management.Infra.Data.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Billing.Management.Infra.Data.Customer.Repository
{
    public sealed class CustomerRepository : RepositoryGeneric<Domain.Customer.Model.Customer>, ICustomerRepository
    {
        public CustomerRepository(ILogger<Domain.Customer.Model.Customer>? logger, BillingApiContext? context) 
            : base(logger, context)
        {
        }

        public Task<IEnumerable<Domain.Customer.Model.Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
