
using Billing.Management.Domain.Product.Repository.Interface;
using Billing.Management.Infra.Data.Context;
using Billing.Management.Infra.Data.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
          return await _context.Products
                        .AsNoTracking()
                        .OrderByDescending(p => p.Id)
                        .Skip((pagenumber - 1) * pagesize)
                        .Take(pagesize)
                        .ToListAsync();
        }
    }
}
