
using Billing.Management.Domain.Product.Repository.Interface;
using Billing.Management.Infra.Data.Context;
using Billing.Management.Infra.Data.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Billing.Management.Infra.Data.Product.Repository
{
    public class ProductRepository : RepositoryGeneric<Domain.Product.Model.Product>, IProductRepository
    {
        public ProductRepository(ILogger<Domain.Product.Model.Product>? logger, BillingApiContext? context) 
            : base(logger, context)
        {
        }

        public async Task<IEnumerable<Domain.Product.Model.Product>> GetAllAsync(int pagenumber, int pagesize)
        {
            var data = await _context.Products
                                .AsNoTracking()
                                .OrderByDescending(p => p.Id)
                                .Skip((pagenumber - 1) * pagesize)
                                .Take(pagesize)
                                .ToListAsync();

            if (data.Count > 0) return data;

            _logger?.LogError(null, "No data found. Try again.");
            throw new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound);
        }

        public bool Exists(Guid id)
            => _context.Products.ToList().Exists(x => x.Id == id);
    }
}
