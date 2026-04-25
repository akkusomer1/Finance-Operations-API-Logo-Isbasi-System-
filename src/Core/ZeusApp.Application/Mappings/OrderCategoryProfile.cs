using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.OrderCategories.Commands.Create;
using ZeusApp.Application.Features.OrderCategories.Commands.Update;
using ZeusApp.Application.Features.OrderCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.OrderCategories.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class OrderCategoryProfile : Profile
{
    public OrderCategoryProfile()
    {
        CreateMap<UpdateOrderCategoryCommand, OrderCategory>().ReverseMap();
        CreateMap<CreateOrderCategoryCommand, OrderCategory>().ReverseMap();
        CreateMap<GetOrderCategoryByIdResponse, OrderCategory>().ReverseMap();
        CreateMap<GetAllOrderCategoriesResponse, OrderCategory>().ReverseMap();
    }
}
