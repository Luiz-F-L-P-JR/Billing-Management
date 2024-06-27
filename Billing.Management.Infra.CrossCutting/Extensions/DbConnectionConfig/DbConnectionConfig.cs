
using Billing.Management.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Management.Infra.CrossCutting.Extensions.DbConnectionConfig
{
    public static class DbConnectionConfig
    {
        public static void AddDbConnection(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<BillingApiContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
        }
    }
}
