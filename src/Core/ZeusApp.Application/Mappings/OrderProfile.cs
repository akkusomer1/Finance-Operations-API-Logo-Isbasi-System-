using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.DTOs.Order;
using ZeusApp.Application.Features.Orders.Commands.Create;
using ZeusApp.Application.Features.Orders.Commands.Update;
using ZeusApp.Application.Features.Orders.Queries.GetAllPaged;
using ZeusApp.Application.Features.Orders.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class OrderProfile:Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderCommand, Order>().ReverseMap();
        CreateMap<UpdateOrderCommand, Order>().ReverseMap();
       
        CreateMap<GetAllOrdersResponse, Order>().ReverseMap();
        CreateMap<CreateProductOrderRequest, ProductOrder>().ReverseMap();
       
        CreateMap<UpdateProductOrderRequest, ProductOrder>().ReverseMap();

        CreateMap<GetOrderByIdResponse, Order>().ReverseMap();
        CreateMap<GetByIdProductOrderResponse, ProductOrder>().ReverseMap();
    }
}
