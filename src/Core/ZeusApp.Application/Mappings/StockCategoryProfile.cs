using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.StockCategories.Commands.Create;
using ZeusApp.Application.Features.StockCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.StockCategories.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class StockCategoryProfile : Profile
{
    public StockCategoryProfile()
    {
        CreateMap<CreateStockCategoryCommand, StockCategory>().ReverseMap();
        CreateMap<GetStockCategoryByIdResponse, StockCategory>().ReverseMap();
        CreateMap<GetAllStockCategoriesResponse, StockCategory>().ReverseMap();
    }
}
