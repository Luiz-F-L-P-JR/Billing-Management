
using Billing.Management.Domain.Billing.Repositories.Interfaces;
using Billing.Management.Infra.Data.Context;
using Billing.Management.Infra.Data.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Billing.Management.Infra.Data.Billing.Repositories
{
    public class BillingRepository : RepositoryGeneric<Domain.Billing.Models.Billing>, IBillingRepository
    {
        public BillingRepository(ILogger<Domain.Billing.Models.Billing>? logger, BillingApiContext? context) 
            : base(logger, context)
        {
        }

        public async Task<IEnumerable<Domain.Billing.Models.Billing>> GetAllAsync(int pagenumber, int pagesize)
        {
            var data = await _context?.Billings
                    .Include(x => x.Customer)
                    .AsNoTracking()
                    .OrderByDescending(c => c.Id)
                    .Skip((pagenumber - 1) * pagesize)
                    .Take(pagesize)
                    .ToListAsync();

            if (data.Count > 0) return data;

            _logger?.LogError(null, "No data found. Try again.");
            throw new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound);
        }

        public async Task<IEnumerable<Domain.Billing.Models.Billing>> GetAllWithLinesAsync(int pagenumber, int pagesize)
        {
            var data = await _context?.Billings
                    .Include(x => x.Customer)
                    .Include(x => x.Lines)
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
            => _context.Billings.ToList().Exists(x => x.Id == id);
    }
}
