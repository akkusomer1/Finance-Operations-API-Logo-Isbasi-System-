using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.DTOs.Invoice;
using ZeusApp.Application.Features.InvoiceCategories.Queries.GetById;
using ZeusApp.Application.Features.Invoices.Commands.Create;
using ZeusApp.Application.Features.Invoices.Commands.Update;
using ZeusApp.Application.Features.Invoices.Queries.GetAllPaged;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<CreateInvoiceCommand, Invoice>().ReverseMap();
        CreateMap<UpdateInvoiceCommand, Invoice>().ReverseMap();
        CreateMap<GetInvoiceByIdResponse, Invoice>().ReverseMap();
        CreateMap<GetAllInvoicesResponse, Invoice>().ReverseMap();
        CreateMap<GetProductInvoiceResponse, ProductInvoice>().ReverseMap();
       
        CreateMap<ProductInvoiceRequest, ProductInvoice>().ReverseMap();
        CreateMap<ProductInvoiceUpdateRequest, ProductInvoice>().ReverseMap();
    }
}
