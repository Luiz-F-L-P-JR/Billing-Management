
using Billing.Management.Domain.Product.Repository.Interface;
using Billing.Management.Infra.Data.Context;
using Billing.Management.Infra.Data.Generic;
using Microsoft.Extensions.Logging;

namespace Billing.Management.Infra.Data.Product.Repository
{
    public class ProductRepository : RepositoryGeneric<Domain.Product.Model.Product>, IProductRepository
    {
        public ProductRepository(ILogger<Domain.Product.Model.Product>? logger, BillingApiContext? context) 
            : base(logger, context)
        {
        }

        public Task<IEnumerable<Domain.Product.Model.Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
