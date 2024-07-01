
using Billing.Management.Domain.Customer.Repository.Interface;
using Billing.Management.Infra.Data.Context;
using Billing.Management.Infra.Data.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

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
            var data = await _context?.Customers
                                .AsNoTracking()
                                .OrderByDescending(c => c.Id)
                                .Skip((pagenumber - 1) * pagesize)
                                .Take(pagesize)
                                .ToListAsync();

            if (data.Count > 0) return data;

            _logger?.LogError(null, "No data found. Try again.");
            throw new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound);
        }

        public bool Exists(Guid id)
            => _context.Customers.ToList().Exists(x => x.Id == id);
    }
}
