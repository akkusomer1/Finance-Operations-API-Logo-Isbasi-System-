using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.InvoiceCategories.Commands.Create;
using ZeusApp.Application.Features.InvoiceCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.InvoiceCategories.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class InvoiceCategoryProfile : Profile
{
    public InvoiceCategoryProfile()
    {
        CreateMap<CreateInvoiceCategoryCommand, InvoiceCategory>().ReverseMap();
        CreateMap<GetInvoiceCategoryByIdResponse, InvoiceCategory>().ReverseMap();
        CreateMap<GetAllInvoiceCategoriesResponse, InvoiceCategory>().ReverseMap();
    }
}
