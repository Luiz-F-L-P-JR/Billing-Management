using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Billing.Management.Infra.CrossCutting.Extensions.ExceptionHandlers
{
    public static class ExceptionHandlersConfig
    {
        public static void AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddControllers(
                option => {
                    option.Filters.Add(typeof(ExceptionFilter));
                }
            ).AddJsonOptions(
                options => {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                }
            );
        }
    }
}
