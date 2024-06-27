
using Billing.Management.Domain.Generic.Repository.Interface;
using Billing.Management.Infra.Data.Context;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Billing.Management.Infra.Data.Generic
{
    public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
    {
        protected readonly ILogger<T>? _logger;
        protected readonly BillingApiContext? _context;

        public RepositoryGeneric(ILogger<T>? logger, BillingApiContext? context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<T> GetAsync(Guid id)
        {
            var data = await _context?.Set<T>().FindAsync(id).AsTask();

            if (data is T) return data;

            _logger?.LogError(null, "No data found. Try again.");
            throw new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound);
        }

        public async Task CreateAsync(T entity)
        {
            if(entity is T)
                await _context.Set<T>().AddAsync(entity);

            _logger?.LogError(null, "Data can not be created.");
            throw new HttpRequestException("Data can not be created.", null, HttpStatusCode.BadRequest);
        }

        public async Task UpdateAsync(T entity)
        {
            if(entity is T)
                await _context.Set<T>().Update(entity).GetDatabaseValuesAsync();

            _logger?.LogError(null, "Data can not be updated.");
            throw new HttpRequestException("Data can not be updated.", null, HttpStatusCode.BadRequest);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await this.GetAsync(id);

            if (entity is T)
                await _context.Set<T>().Remove(entity).GetDatabaseValuesAsync();

            _logger?.LogError(null, "Data can not be deleted.");
            throw new HttpRequestException("Data can not be deleted.", null, HttpStatusCode.BadRequest);
        }
    }
}
