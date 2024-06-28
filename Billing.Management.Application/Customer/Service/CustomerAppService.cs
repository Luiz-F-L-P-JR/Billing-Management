
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

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync(int pagenumber, int pagesize)
        {
            var customers = await _service?.GetAllAsync(pagenumber, pagesize);
            return _mapper?.Map<List<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> GetAsync(Guid id)
        {
            var customer = await _service?.GetAsync(id);
            return _mapper?.Map<CustomerDTO>(customer);
        }

        public async Task CreateAsync(CustomerDTO entity)
        {
            var customer = _mapper.Map<Domain.Customer.Model.Customer>(entity);

            customer.Id = Guid.NewGuid();
            await _service?.CreateAsync(customer);
        }

        public async Task UpdateAsync(CustomerDTO entity)
        {
            var customer = _mapper.Map<Domain.Customer.Model.Customer>(entity);
            await _service?.UpdateAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _service?.DeleteAsync(id);
        }
    }
}
