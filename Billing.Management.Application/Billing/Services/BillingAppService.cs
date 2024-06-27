
using AutoMapper;
using Billing.Management.Application.Billing.DTOs;
using Billing.Management.Application.Billing.Services.Interfaces;
using Billing.Management.Domain.Billing.Services.Interfaces;

namespace Billing.Management.Application.Billing.Services
{
    public class BillingAppService : IBillingAppService
    {
        private readonly IMapper? _mapper;
        private readonly IBillingDomainService? _service;

        public BillingAppService(IMapper? mapper, IBillingDomainService? service)
        {
            _mapper = mapper;
            _service = service;
        }

        public Task<IEnumerable<BillingDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BillingDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(BillingDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(BillingDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
