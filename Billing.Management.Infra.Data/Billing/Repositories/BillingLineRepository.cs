
using Billing.Management.Domain.Billing.Models;
using Billing.Management.Domain.Billing.Repositories.Interfaces;
using Billing.Management.Infra.Data.Context;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Billing.Management.Infra.Data.Billing.Repositories
{
    public class BillingLineRepository : IBillingLineRepository
    {
        private readonly ILogger<BillingLine>? _logger;
        private readonly BillingApiContext? _context;

        public BillingLineRepository(ILogger<BillingLine>? logger, BillingApiContext? context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task CreateAsync(BillingLine entity)
        {
            if (entity is not BillingLine)
            {
                _logger?.LogError(null, "Data can not be created.");
                throw new HttpRequestException("Data can not be created.", null, HttpStatusCode.BadRequest);
            }

            await _context.BillingLines.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.BillingLines.FindAsync(id);

            if (entity is not BillingLine)
            {
                _logger?.LogError(null, "Data can not be deleted.");
                throw new HttpRequestException("Data can not be deleted.", null, HttpStatusCode.BadRequest);
            }

            _context?.BillingLines?.Remove(entity);
        }

        public bool Exists(Guid id)
            => _context.BillingLines.ToList().Exists(x => x.Id == id);
    }
}
