using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Management.Infra.CrossCutting.Extensions.SwaggerConfig
{
    public static class Swagger
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("Billing.Management.Api", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Billing.Management.Api",
                    Description = "API REST para gerenciamento de serviços de faturamento.",

                    Contact = new OpenApiContact
                    {
                        Name = "Luiz Fernando Junior.",
                        Email = "luizfernandojr1998@gmail.com"
                    }
                });
            });
        }
    }
}
