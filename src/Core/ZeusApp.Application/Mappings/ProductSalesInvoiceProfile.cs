using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.ProductInvoices.Commands.Create;
using ZeusApp.Application.Features.ProductInvoices.Queries.GetAllPaged;
using ZeusApp.Application.Features.ProductInvoices.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class ProductInvoiceProfile : Profile
{
    public ProductInvoiceProfile()
    {
        CreateMap<CreateProductInvoiceCommand, ProductInvoice>().ReverseMap();
        CreateMap<GetProductInvoiceByIdResponse, ProductInvoice>().ReverseMap();
        CreateMap<GetAllProductInvoicesResponse, ProductInvoice>().ReverseMap();
    }
}
