
using Billing.Management.Domain.Billing.Models;
using Billing.Management.Domain.Billing.Repositories.Interfaces;
using Billing.Management.Infra.Data.Context;
using Billing.Management.Infra.Data.Generic;
using Microsoft.Extensions.Logging;

namespace Billing.Management.Infra.Data.Billing.Repositories
{
    public class BillingLineRepository : RepositoryGeneric<BillingLine>, IBillingLineRepository
    {
        public BillingLineRepository(ILogger<BillingLine>? logger, BillingApiContext? context) 
            : base(logger, context)
        {
        }

        public Task<IEnumerable<BillingLine>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
