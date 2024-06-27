
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

        public Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ProductDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ProductDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
