using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.DTOs.Order;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Application.Features.InvoiceCategories.Queries.GetById;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.Orders.Queries.GetById;

public class GetOrderByIdQuery : IRequest<Result<GetOrderByIdResponse>>
{
    public int Id { get; set; }
}


public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<GetOrderByIdResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOtherAddressRepository _otherAddressRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IOtherAddressRepository otherAddressRepository)
    {
        _orderRepository = orderRepository;
        _otherAddressRepository = otherAddressRepository;
    }

    public async Task<Result<GetOrderByIdResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);

        var  orderMapped = _mapper.Map<GetOrderByIdResponse>(order);

        if (order.OtherAddressId != null && order.IsAddressDifferent)
        {
            var otherAdress = await _otherAddressRepository.GetByIdAsync(order.OtherAddressId.Value);
            CustomerOtherAddressResponse otherAdressResp = new()
            {
                Id = request.Id,
                Address = otherAdress.Address,
                AddressTitle = otherAdress.AddressTitle,
                City = otherAdress.City,
                Country = otherAdress.Country,
                District = otherAdress.District
            };
            orderMapped.CustomerOtherAddressResponse = otherAdressResp;
        }
        return Result<GetOrderByIdResponse>.Success(orderMapped, 200);
    }
}
