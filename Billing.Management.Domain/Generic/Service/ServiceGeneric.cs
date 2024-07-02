using Billing.Management.Domain.Generic.Service.Interface;
using Billing.Management.Domain.UnitOfWork.Interface;

namespace Billing.Management.Domain.Generic.Service
{
    public class ServiceGeneric<T> : IServiceGeneric<T> where T : class
    {
        protected readonly IUnitOfWork<T>? _unitOfWork;

        public ServiceGeneric(IUnitOfWork<T>? unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<T> GetAsync(Guid id) 
            => await _unitOfWork?.RepositoryGeneric?.GetAsync(id);

        public async Task CreateAsync(T entity)
        {
            await _unitOfWork?.RepositoryGeneric?.CreateAsync(entity);
            await _unitOfWork?.Commit();
        }

        public async Task UpdateAsync(T entity)
        {
            await _unitOfWork?.RepositoryGeneric?.UpdateAsync(entity);
            await _unitOfWork?.Commit();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork?.RepositoryGeneric?.DeleteAsync(id);
            await _unitOfWork?.Commit();
        }
    }
}
