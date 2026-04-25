using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.ProductStocks.Commands.Create;
using ZeusApp.Application.Features.ProductStocks.Queries.GetAllPaged;
using ZeusApp.Application.Features.ProductStocks.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class ProductStockProfile : Profile
{
    public ProductStockProfile()
    {
        CreateMap<CreateProductStockCommand, ProductStock>().ReverseMap();
        CreateMap<GetProductStockByIdResponse, ProductStock>().ReverseMap();
        CreateMap<GetAllProductStocksResponse, ProductStock>().ReverseMap();
    }
}
