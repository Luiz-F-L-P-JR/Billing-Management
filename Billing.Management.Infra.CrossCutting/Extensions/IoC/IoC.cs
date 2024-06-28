
using Billing.Management.Application.Billing.Services.Interfaces;
using Billing.Management.Application.Billing.Services;
using Billing.Management.Application.Customer.Service.Interface;
using Billing.Management.Application.Customer.Service;
using Billing.Management.Application.Product.Service.Interface;
using Billing.Management.Application.Product.Service;
using Billing.Management.Domain.Billing.Services;
using Billing.Management.Domain.Billing.Services.Interfaces;
using Billing.Management.Domain.Customer.Service;
using Billing.Management.Domain.Customer.Service.Interface;
using Billing.Management.Domain.Generic.Repository.Interface;
using Billing.Management.Domain.Generic.Service;
using Billing.Management.Domain.Generic.Service.Interface;
using Billing.Management.Domain.Product.Service;
using Billing.Management.Domain.Product.Service.Interface;
using Billing.Management.Infra.Data.Generic;
using Microsoft.Extensions.DependencyInjection;
using Billing.Management.Domain.UnitOfWork.Interface;
using Billing.Management.Infra.Data.UnitOfWork;
using Billing.Management.Domain.Product.Repository.Interface;
using Billing.Management.Infra.Data.Product.Repository;
using Billing.Management.Domain.Billing.Repositories.Interfaces;
using Billing.Management.Infra.Data.Billing.Repositories;
using Billing.Management.Domain.Customer.Repository.Interface;
using Billing.Management.Infra.Data.Customer.Repository;
using Billing.Management.Application.AutoMapper;
using Billing.Management.Domain.HttpHandler.Interface;
using Billing.Management.Infra.Data.HttpHandler;

namespace Billing.Management.Infra.CrossCutting.Extensions.IoC
{
    public static class IoC
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            #region UnitOfWork Injection
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            #endregion

            #region Application Injection
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IBillingAppService, BillingAppService>();
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            #endregion

            #region Domain Injection
            services.AddScoped<IProductDomainService, ProductDomainService>();
            services.AddScoped<IBillingDomainService, BillingDomainService>();
            services.AddScoped<ICustomerDomainService, CustomerDomainService>();
            #endregion

            #region Data Injection
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBillingRepository, BillingRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBillingLineRepository, BillingLineRepository>();
            #endregion

            #region Generics Injection
            services.AddScoped(typeof(IServiceGeneric<>), typeof(ServiceGeneric<>));
            services.AddScoped(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>));
            #endregion

            #region AutoMapper Injection

            services.AddAutoMapper(typeof(AutoMapperProfile));

            #endregion

            #region Http Injection
            services.AddScoped(typeof(HttpClient));
            services.AddScoped<IHttpRequests, HttpRequests>();
            #endregion
        }
    }
}
