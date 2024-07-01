
using Billing.Management.Domain.Billing.Models;
using Billing.Management.Domain.Billing.Services.Interfaces;
using Billing.Management.Domain.Generic.Service;
using Billing.Management.Domain.UnitOfWork.Interface;
using System.Net;

namespace Billing.Management.Domain.Billing.Services
{
    public sealed class BillingDomainService : ServiceGeneric<Models.Billing>, IBillingDomainService
    {

        public BillingDomainService(IUnitOfWork<Models.Billing>? unitOfWork) 
            : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Models.Billing>> GetAllAsync(int pagenumber, int pagesize)
            => await _unitOfWork?.BillingRepository.GetAllAsync(pagenumber, pagesize);

        public async Task<IEnumerable<Models.Billing>> GetAllWithLinesAsync(int pagenumber, int pagesize)
            => await _unitOfWork?.BillingRepository.GetAllWithLinesAsync(pagenumber, pagesize);

        public async new Task CreateAsync(Models.Billing entity)
        {
            //This method generates an Id to avoid
            //the default Id that the API generate,
            //that is always the same.
            entity = GenerateBilingsId(entity);

            await ValidateCustomer(entity.Customer);
            await CreateLinesAsync(entity.Lines);

            entity.Customer = null;

            await _unitOfWork?.BillingRepository.CreateAsync(entity);

            await _unitOfWork.Commit();
        }

        private async Task CreateLinesAsync(IList<BillingLine> entities)
        {
            entities.ToList().ForEach(
                async x =>
                {
                    Product.Model.Product product = new()
                    { 
                        Id = x.ProductId, 
                        Name = x.Description 
                    };

                    await ValidateProduct(product);

                    await _unitOfWork.BillingLinesRepository.CreateAsync(x);
                }
            );
        }

        public async new Task DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork?.BillingRepository.GetAsync(id);

            await DeleteLinesLinesAsync(entity.Lines);

            await _unitOfWork?.BillingRepository.DeleteAsync(id);

            await _unitOfWork.Commit();
        }

        private async Task DeleteLinesLinesAsync(IList<BillingLine> entity)
        {
            foreach (var line in entity)
            {
                await _unitOfWork?.BillingLinesRepository?.DeleteAsync(line.Id);
            }
        }

        #region Helpers

        private async Task ValidateCustomer(Customer.Model.Customer customer)
        {
            bool customerExists = _unitOfWork.CustomerRepository.Exists(customer.Id);

            if (!customerExists)
            {
                await _unitOfWork.CustomerRepository.CreateAsync(customer);
                await _unitOfWork.Commit();

                throw new HttpRequestException("Customer doesn't exist. We gonna registrate him/her. Please try again.", null, HttpStatusCode.BadRequest);
            }
        }

        private async Task ValidateProduct(Product.Model.Product product)
        {
            bool productExists = _unitOfWork.ProductRepository.Exists(product.Id);

            if (!productExists)
            {
                await _unitOfWork.ProductRepository.CreateAsync(product);
                await _unitOfWork.Commit();

                throw new HttpRequestException("Product doesn't exist. We gonna registrate it. Please try again.", null, HttpStatusCode.BadRequest);
            }
        }

        //Creates new random ids, to avoid the default id guid created from the api.
        private Models.Billing GenerateBilingsId(Models.Billing billing)
        {
            billing.Id = Guid.NewGuid();

            billing.Lines.ToList().ForEach(
                x =>
                {
                    x.Id = Guid.NewGuid();
                    x.BillingId = billing.Id;
                }
            );

            return billing;
        }

        #endregion
    }
}
