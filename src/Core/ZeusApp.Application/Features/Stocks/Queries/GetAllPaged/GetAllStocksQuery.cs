using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.Stocks.Queries.GetAllPaged;
public class GetAllStocksQuery:IRequest<PaginatedResult<GetAllStocksResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<Stock, bool>> Filter { get; set; }

    public GetAllStocksQuery(int pageNumber, int pageSize, Expression<Func<Stock, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}
public class GetAllStocksQueryHandler : IRequestHandler<GetAllStocksQuery, PaginatedResult<GetAllStocksResponse>>
{
    private readonly IStockRepository _repository;

    public GetAllStocksQueryHandler(IStockRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GetAllStocksResponse>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Stock, GetAllStocksResponse>> expression = e => new GetAllStocksResponse
        {
            Id = e.Id,
            GrandTotal = e.GrandTotal,
            Date = e.Date,
            MovementType = e.MovementType,
            Status = e.Status == EntityStatus.Active ? "Aktif" : "Aktif değil",
            CustomerOrSupplierName = "",
            LastModifiedOn = e.LastModifiedOn
        };

        //CustomerOrSupplierName: alış ve satış faturasında bu stockIn ve stockOut tablosuna eklenecek bu durumda dolu gelecek bu alan.
        PaginatedResult<GetAllStocksResponse> paginatedList;

        if (request.Filter == null)
        {
            paginatedList = await _repository.Stocks
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            paginatedList = await _repository.Stocks
                .Where(request.Filter)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        return paginatedList;
    }
}
