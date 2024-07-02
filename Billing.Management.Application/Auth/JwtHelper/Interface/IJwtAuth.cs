
using Billing.Management.Domain.Auth.Model;

namespace Billing.Management.Application.Auth.JwtHelper.Interface
{
    public interface IJwtAuth
    {
        Task<string> GenerateToken(UserAuth user);
    }
}
