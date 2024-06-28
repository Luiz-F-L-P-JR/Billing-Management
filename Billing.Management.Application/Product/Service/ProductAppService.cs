
using AutoMapper;
using Billing.Management.Application.Product.DTO;
using Billing.Management.Application.Product.Service.Interface;
using Billing.Management.Domain.Product.Service.Interface;

namespace Billing.Management.Application.Product.Service
{
    public class ProductAppService : IProductAppService
    {
        private readonly IMapper? _mapper;
        private readonly IProductDomainService? _service;

        public ProductAppService(IMapper? mapper, IProductDomainService? service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync(int pagenumber, int pagesize)
        {
            var products = await _service?.GetAllAsync(pagenumber, pagesize);
            return _mapper?.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetAsync(Guid id)
        {
            var product = await _service?.GetAsync(id);
            return _mapper?.Map<ProductDTO>(product);
        }

        public async Task CreateAsync(ProductDTO entity)
        {
            var product = _mapper.Map<Domain.Product.Model.Product>(entity);
            await _service?.CreateAsync(product);
        }

        public async Task UpdateAsync(ProductDTO entity)
        {
            var product = _mapper.Map<Domain.Product.Model.Product>(entity);
            await _service?.CreateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _service?.DeleteAsync(id);
        }
    }
}
