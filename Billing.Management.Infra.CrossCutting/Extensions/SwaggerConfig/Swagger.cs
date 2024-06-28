using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Billing.Management.Infra.CrossCutting.Extensions.SwaggerConfig
{
    public static class Swagger
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
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

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Billing.Management.Api.xml"));
            });
        }

        public static void UseSwaggerConfig(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
        }
    }
}
