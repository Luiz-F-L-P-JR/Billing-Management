
using Billing.Management.Domain.Customer.Repository.Interface;
using Billing.Management.Infra.Data.Context;
using Billing.Management.Infra.Data.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Billing.Management.Infra.Data.Customer.Repository
{
    public sealed class CustomerRepository : RepositoryGeneric<Domain.Customer.Model.Customer>, ICustomerRepository
    {
        public CustomerRepository(ILogger<Domain.Customer.Model.Customer>? logger, BillingApiContext? context) 
            : base(logger, context)
        {
        }

        public async Task<IEnumerable<Domain.Customer.Model.Customer>> GetAllAsync(int pagenumber, int pagesize)
        {
            return await _context?.Customers
                            .AsNoTracking()
                            .OrderByDescending(c => c.Id)
                            .Skip((pagenumber - 1) * pagesize)
                            .Take(pagesize)
                            .ToListAsync();
        }
    }
}
