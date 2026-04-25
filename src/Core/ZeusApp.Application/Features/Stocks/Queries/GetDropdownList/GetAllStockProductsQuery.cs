using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Features.Stocks.Queries.GetAllPaged;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.Stocks.Queries.GetDropdownList;
public class GetAllStockProductsQuery : IRequest<Result<List<GetAllStockProductsResponse>>>
{  
}



public class GetAllStockProductsQueryHandler : IRequestHandler<GetAllStockProductsQuery, Result<List<GetAllStockProductsResponse>>>
{
    private readonly IProductServiceRepository _repository;

    public GetAllStockProductsQueryHandler(IProductServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<GetAllStockProductsResponse>>> Handle(GetAllStockProductsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<ProductService, GetAllStockProductsResponse>> expression = e => new GetAllStockProductsResponse
        {
            ProductServiceId = e.Id,
            UnitId = e.Unit.Id,
            ProductName = e.Name,
            PurchasePriceExcludingVAT = e.PurchasePriceExcludingVAT,
            UnitName = e.Unit.Name,
            TotalStockAmount = e.TotalStockAmount
        };

        var listProductStocks = await _repository.ProductServices
            .Where(x=>x.ProductServiceType==Domain.Enums.ProductServiceType.product)
            .Select(expression).ToListAsync();

        return  Result<List<GetAllStockProductsResponse>>.Success(listProductStocks, 200);
    }

   
}