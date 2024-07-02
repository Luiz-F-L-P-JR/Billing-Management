
using AutoMapper;
using Billing.Management.Application.Billing.DTOs;
using Billing.Management.Application.Billing.HttpRequests.DTOs;
using Billing.Management.Application.Customer.DTO;
using Billing.Management.Application.Product.DTO;
using Billing.Management.Domain.Billing.Models;

namespace Billing.Management.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Domain.Product.Model.Product, ProductDTO>().ReverseMap();
            CreateMap<Domain.Customer.Model.Customer, CustomerDTO>().ReverseMap();

            CreateMap<BillingLine, BillingLineDTO>().ReverseMap();
            CreateMap<BillingLine, BillingLineRequestDTO>().ReverseMap();
            CreateMap<Domain.Billing.Models.Billing, BillingDTO>().ReverseMap();
            CreateMap<Domain.Billing.Models.Billing, BillingRequestDTO>().ReverseMap();
        }
    }
}
