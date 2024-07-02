
using AutoMapper;
using Billing.Management.Application.Billing.DTOs;
using Billing.Management.Application.Billing.HttpRequests.Interface;
using Billing.Management.Application.Billing.Services.Interfaces;
using Billing.Management.Domain.Billing.Services.Interfaces;

namespace Billing.Management.Application.Billing.Services
{
    public class BillingAppService : IBillingAppService
    {
        private readonly IMapper? _mapper;
        private readonly IBillingDomainService? _service;
        private readonly IBillingServiceRequests? _requests;

        public BillingAppService(
            IMapper? mapper, 
            IBillingDomainService? service, 
            IBillingServiceRequests? requests)
        {
            _mapper = mapper;
            _service = service;
            _requests = requests;
        }

        public async Task<IEnumerable<BillingDTO>> GetAllAsync(int pagenumber, int pagesize)
        {
            var billings = await _service?.GetAllAsync(pagenumber, pagesize);

            return _mapper?.Map<IList<BillingDTO>>(billings);
        }

        public async Task<IEnumerable<BillingDTO>> GetAllWithLinesAsync(int pagenumber, int pagesize)
        {
            var billings = await _service?.GetAllWithLinesAsync(pagenumber, pagesize);

            return _mapper?.Map<IList<BillingDTO>>(billings);
        }

        public async Task<BillingDTO> GetAsync(Guid id)
        {
            var billing = await _service?.GetAsync(id);

            return _mapper?.Map<BillingDTO>(billing);
        }

        public async Task CreateAsync(BillingDTO entity)
        {
            var billing = _mapper?.Map<Domain.Billing.Models.Billing>(entity);
            await _service?.CreateAsync(billing);
        }

        public async Task CreateFromRequestAsync()
        {
            var result = await _requests?.GetAllAsync();

            var billings = _mapper?.Map<IList<Domain.Billing.Models.Billing>>(result);

            if (billings.Count > 0)
            {
                foreach (var billing in billings)
                {
                    await _service?.CreateAsync(billing);
                }
            }

            throw new ArgumentException("");
        }

        public async Task UpdateAsync(BillingDTO entity)
        {
            var billing = _mapper?.Map<Domain.Billing.Models.Billing>(entity);
            await _service?.UpdateAsync(billing);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _service?.DeleteAsync(id);
        }
    }
}
