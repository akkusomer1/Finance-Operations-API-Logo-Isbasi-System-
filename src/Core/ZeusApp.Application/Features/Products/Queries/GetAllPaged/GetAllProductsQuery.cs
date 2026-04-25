using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Products.Queries.GetAllPaged;
public class GetAllProductsQuery : IRequest<PaginatedResult<GetAllProductsResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<ProductService, bool>> Filter { get; set; }

    public GetAllProductsQuery(int pageNumber, int pageSize, Expression<Func<ProductService, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResult<GetAllProductsResponse>>
{
    private readonly IProductServiceRepository _repository;

    public GetAllProductsQueryHandler(IProductServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GetAllProductsResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<ProductService, GetAllProductsResponse>> expression = e => new GetAllProductsResponse
        {
            Id = e.Id,
            Code = e.Code,
            Name = e.Name,
            PurchasePriceIncludingVAT = e.PurchasePriceIncludingVAT,
            SalesPriceIncludingVAT = e.SalesPriceIncludingVAT,
            TotalStockAmount = e.TotalStockAmount,
            UnitName = e.Unit.Name,
            VATRate = e.VATRate,
        };


        PaginatedResult<GetAllProductsResponse> paginatedList;
        if (request.Filter == null)
        {
            paginatedList = await _repository.ProductServices
                .Where(x=>x.ProductServiceType==ProductServiceType.product)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
     
        else
        {
            paginatedList = await _repository.ProductServices
                .Where(request.Filter)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        return paginatedList;
    }
}
