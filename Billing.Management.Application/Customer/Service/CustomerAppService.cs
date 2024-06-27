
using AutoMapper;
using Billing.Management.Application.Customer.DTO;
using Billing.Management.Application.Customer.Service.Interface;
using Billing.Management.Domain.Customer.Service.Interface;

namespace Billing.Management.Application.Customer.Service
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper? _mapper;
        private readonly ICustomerDomainService? _service;

        public CustomerAppService(IMapper? mapper, ICustomerDomainService? service)
        {
            _mapper = mapper;
            _service = service;
        }

        public Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(CustomerDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomerDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
