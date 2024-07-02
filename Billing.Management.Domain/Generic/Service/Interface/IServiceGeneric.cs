
namespace Billing.Management.Domain.Generic.Service.Interface
{
    public interface IServiceGeneric<T>
    {
        Task DeleteAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetAsync(Guid id);
    }
}
