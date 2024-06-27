
using AutoMapper;
using Billing.Management.Application.Billing.DTOs;
using Billing.Management.Application.Customer.DTO;
using Billing.Management.Application.Product.DTO;
using Billing.Management.Domain.Billing.Models;

namespace Billing.Management.Application.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<BillingLine, BillingLineDTO>().ReverseMap();
            CreateMap<Domain.Product.Model.Product, ProductDTO>().ReverseMap();
            CreateMap<Domain.Billing.Models.Billing, BillingDTO>().ReverseMap();
            CreateMap<Domain.Customer.Model.Customer, CustomerDTO>().ReverseMap();
        }
    }
}
