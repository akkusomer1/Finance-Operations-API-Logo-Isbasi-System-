using AutoMapper;
using ZeusApp.Application.DTOs.Stock;
using ZeusApp.Application.Features.Stocks.Commands.Create;
using ZeusApp.Application.Features.Stocks.Commands.Update;
using ZeusApp.Application.Features.Stocks.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class StockProfile:Profile
{
    public StockProfile()
    {
        CreateMap<CreateStockInCommand,Stock>().ReverseMap();
        CreateMap<CreateStockOutCommand,Stock>().ReverseMap();
        CreateMap<UpdateStockCommand,Stock>().ReverseMap();
        CreateMap<StoctProductUpdateRequest, Stock>().ReverseMap();

        CreateMap<GetStockByIdResponse, Stock>().ReverseMap();
        CreateMap<ProductStockResponse, ProductStock>().ReverseMap();
    }
}
