using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Features.Invoices.Queries.GetAllPaged;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Orders.Queries.GetAllPaged;
public class GetAllOrdersQuery : IRequest<PaginatedResult<GetAllOrdersResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<Order, bool>> Filter { get; set; }
    public InvoiceType InvoiceType { get; set; }
    public GetAllOrdersQuery(int pageNumber, int pageSize, Expression<Func<Order, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}


public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PaginatedResult<GetAllOrdersResponse>>
{
    private readonly IOrderRepository _repository;
    public GetAllOrdersQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GetAllOrdersResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Order, GetAllOrdersResponse>> expression = e => new GetAllOrdersResponse
        {
            Id = e.Id,
            HasPaid = e.HasPaid,
            OrderCategoryName=e.OrderCategory!.Name,
            OrderDate=e.OrderDate,
            OrderNumber=e.OrderNumber,
            OrderStatus=e.OrderStatus,
            TotalAmount=e.TotalAmount,
            NameOrTitle = e.CustomerSupplier.GeneralType == GeneralType.Individual
            ? $"{e.CustomerSupplier.FirstName} {e.CustomerSupplier.LastName}" : e.CustomerSupplier.Title,
            ExchanceCalculate = e.CurrencyType != CurrencyType.TL ? $"{e.ExchangeRate * e.TotalAmount} {e.CurrencyType.ToString()}" : null,        
        };


        PaginatedResult<GetAllOrdersResponse> paginatedList;
        if (request.Filter == null)
        {
            paginatedList = await _repository.Orders
                .Include(x=>x.OrderCategory)
                .Include(x=>x.CustomerSupplier)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            paginatedList = await _repository.Orders
                .Where(request.Filter)
                .Include(x => x.OrderCategory)
                .Include(x => x.CustomerSupplier)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        return paginatedList;
    }
}
