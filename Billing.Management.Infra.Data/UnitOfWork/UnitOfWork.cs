
using Billing.Management.Domain.Billing.Models;
using Billing.Management.Domain.Billing.Repositories.Interfaces;
using Billing.Management.Domain.Customer.Repository.Interface;
using Billing.Management.Domain.Generic.Repository.Interface;
using Billing.Management.Domain.Product.Repository.Interface;
using Billing.Management.Domain.UnitOfWork.Interface;
using Billing.Management.Infra.Data.Billing.Repositories;
using Billing.Management.Infra.Data.Context;
using Billing.Management.Infra.Data.Customer.Repository;
using Billing.Management.Infra.Data.Generic;
using Billing.Management.Infra.Data.Product.Repository;
using Microsoft.Extensions.Logging;

namespace Billing.Management.Infra.Data.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly ILogger<T>? _logger;
        protected readonly BillingApiContext? _context;
        private readonly ILogger<BillingLine>? _loggerLines;
        private readonly ILogger<Domain.Product.Model.Product>? _loggerProduct;
        private readonly ILogger<Domain.Billing.Models.Billing>? _loggerBilling;
        private readonly ILogger<Domain.Customer.Model.Customer>? _loggerCustomer;

        private IRepositoryGeneric<T>? _repositoryGeneric;

        private IBillingRepository? _billingRepository;
        private IProductRepository? _productRepository;
        private ICustomerRepository? _customerRepository;
        private IBillingLineRepository? _billingLineRepository;

        public UnitOfWork(
            ILogger<T>? logger, 
            BillingApiContext? context,
            ILogger<BillingLine>? loggerLines,
            ILogger<Domain.Product.Model.Product>? loggerProduct,
            ILogger<Domain.Billing.Models.Billing>? loggerBilling, 
            ILogger<Domain.Customer.Model.Customer>? loggerCustomer
            )
        {
            _logger = logger;
            _context = context;
            _loggerLines = loggerLines;
            _loggerProduct = loggerProduct;
            _loggerBilling = loggerBilling;
            _loggerCustomer = loggerCustomer;
        }

        public IRepositoryGeneric<T> RepositoryGeneric => _repositoryGeneric ?? new RepositoryGeneric<T>(_logger, _context);

        public IBillingRepository BillingRepository => _billingRepository ?? new BillingRepository(_loggerBilling, _context);
        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_loggerProduct, _context);
        public ICustomerRepository CustomerRepository => _customerRepository ?? new CustomerRepository(_loggerCustomer, _context);
        public IBillingLineRepository BillingLinesRepository => _billingLineRepository ?? new BillingLineRepository(_loggerLines, _context);

        public async Task Commit() => await _context?.SaveChangesAsync();

        public async Task Dispose() => await _context?.DisposeAsync().AsTask();
    }
}
