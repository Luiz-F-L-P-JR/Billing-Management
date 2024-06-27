
using Billing.Management.Domain.Billing.Repositories.Interfaces;
using Billing.Management.Domain.Customer.Repository.Interface;
using Billing.Management.Domain.Generic.Repository.Interface;
using Billing.Management.Domain.Product.Repository.Interface;

namespace Billing.Management.Domain.UnitOfWork.Interface
{
    public interface IUnitOfWork<T>
    {
        IRepositoryGeneric<T> RepositoryGeneric { get; }

        IBillingRepository BillingRepository { get; }
        IProductRepository ProductRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IBillingLineRepository BillingLinesRepository { get; }

        Task Commit();
    }
}
