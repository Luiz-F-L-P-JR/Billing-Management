
using Billing.Management.Domain.Auth.Model;
using Billing.Management.Domain.Auth.Repository.Interface;
using Billing.Management.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Billing.Management.Infra.Data.Auth.Repository
{
    public class UserAuthRegister : IUserAuthRegister
    {
        protected readonly ILogger<UserAuth>? _logger;
        protected readonly BillingApiContext? _context;

        public UserAuthRegister(ILogger<UserAuth>? logger, BillingApiContext? context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<UserAuth> GetAsync(string email)
        {
            bool verifyUser = _context.Users.ToList().Exists(x => x.Email.ToLower() == email);

            if (verifyUser)
            {
                var data = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email);
                return data;
            }

            _logger?.LogError(null, "No data found. Try again.");
            throw new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound);
        }

        public async Task CreateAsync(UserAuth entity)
        {
            if (entity is not UserAuth)
            {
                _logger?.LogError(null, "Data can not be created.");
                throw new HttpRequestException("Data can not be created.", null, HttpStatusCode.BadRequest);
            }

            await _context.Users.AddAsync(entity);
            await _context?.SaveChangesAsync();
        }
    }
}
