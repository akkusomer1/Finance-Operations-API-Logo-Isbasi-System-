using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.CorporateCustomerSupplieries.Queries.GetById;
using ZeusApp.Application.Features.CustomerSupplieries.Commands.Create;
using ZeusApp.Application.Features.CustomerSupplieries.Commands.Update;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class CustomerSupplierProfile:Profile
{
    public CustomerSupplierProfile()
    {
        CreateMap<CreateCustomerSupplierCommand, CustomerSupplier>().ReverseMap();
        CreateMap<UpdateCustomerSupplierCommand, CustomerSupplier>().ReverseMap();
        CreateMap<GetCustomerSupplierByIdResponse, CustomerSupplier>().ReverseMap();
    }
}
