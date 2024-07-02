
namespace Billing.Management.Domain.Generic.Repository.Interface
{
    public interface IRepositoryGeneric<T>
    {
        Task DeleteAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetAsync(Guid id);
    }
}
