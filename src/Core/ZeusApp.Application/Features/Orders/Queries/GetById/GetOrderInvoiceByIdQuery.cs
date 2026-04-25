using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.DTOs.Invoice;
using ZeusApp.Application.DTOs.Order;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Application.Features.InvoiceCategories.Queries.GetById;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.Orders.Queries.GetById;
public class GetOrderInvoiceByIdQuery : IRequest<Result<GetOrderInvoiceByIdResponse>>
{
    public int OrderId { get; set; }
}


public class GetOrderInvoiceByIdQueryHandler : IRequestHandler<GetOrderInvoiceByIdQuery, Result<GetOrderInvoiceByIdResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderInvoiceByIdQueryHandler( IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<GetOrderInvoiceByIdResponse>> Handle(GetOrderInvoiceByIdQuery request, CancellationToken cancellationToken)
    {

        var order = await _orderRepository.Orders
            .Include(x => x.CustomerSupplier)
            .Include(x => x.ProductOrders)
            .ThenInclude(x => x.ProductService)
            .SingleOrDefaultAsync(x => x.Id == request.OrderId);

        //Müşterinin diğer adreslerinide gönder.


        var getInvoiceOrderByIdResponse = new GetOrderInvoiceByIdResponse()
        {
            OrderId = order.Id,
            CustomerSupplierId = order.CustomerSupplierId,
            CurrencyType = order.CurrencyType,
            DiscountAmount = order.DiscountAmount,
            DiscountType = order.DiscountType,
            ExchangeRate = order.ExchangeRate,
            TotalVATAmount = order.TotalVATAmount,
            TotalDiscount = order.TotalDiscount,
            TotalAmount = order.TotalAmount,
            Total = order.TotalAmount,
            Subtotal = order.Subtotal,
            InvoiceType = order.InvoiceType,
            OrderInvoiceType = order.OrderInvoiceType
        };

        foreach (var item in order.ProductOrders)
        {
            var productOrder = new GetProductOrderResponse
            {
                ProductOrderId=item.Id,
                Description = item.Description,
                Discount = item.Discount,
                DiscountType=item.DiscountType, 
                ProductAmount = item.ProductAmount,
                ProductServiceId = item.ProductServiceId,
                TaxAmount = item.TaxAmount, 
                UnitId = item.UnitId,   
                UnitPrice = item.UnitPrice,
                TaxRate = item.TaxRate,
                TotalSalesAmountForProduct=item.TotalSalesAmountForProduct,                
            };
            getInvoiceOrderByIdResponse.ProductOrders.Add(productOrder);
        }

        return Result<GetOrderInvoiceByIdResponse>.Success(getInvoiceOrderByIdResponse, 200);
    }
}