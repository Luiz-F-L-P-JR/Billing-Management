
using Billing.Management.Domain.Auth.Model;

namespace Billing.Management.Domain.Auth.Repository.Interface
{
    public interface IUserAuthRegister
    {
        Task CreateAsync(UserAuth entity);
        Task<UserAuth> GetAsync(string email);
    }
}
