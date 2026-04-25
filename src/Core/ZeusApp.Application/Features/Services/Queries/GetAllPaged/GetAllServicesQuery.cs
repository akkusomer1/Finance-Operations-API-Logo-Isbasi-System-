using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Features.Services.Queries.GetAllPaged;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Services.Queries.GetAllPaged;
public class GetAllServicesQuery:IRequest<PaginatedResult<GetAllServicesResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<ProductService, bool>> Filter { get; set; }

    public GetAllServicesQuery(int pageNumber, int pageSize, Expression<Func<ProductService, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}


public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, PaginatedResult<GetAllServicesResponse>>
{
    private readonly IProductServiceRepository _repository;

    public GetAllServicesQueryHandler(IProductServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GetAllServicesResponse>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<ProductService, GetAllServicesResponse>> expression = e => new GetAllServicesResponse
        {
           Id = e.Id,   
           Code = e.Code,
           Name = e.Name,
           PurchasePriceIncludingVAT=e.PurchasePriceIncludingVAT,   
           SalesPriceIncludingVAT= e.SalesPriceIncludingVAT,    
           ServiceGroupId = e.ServiceGroupId,
           Status = e.Status,
           UnitId = e.UnitId,
           VATRate=e.VATRate,
        };


        PaginatedResult<GetAllServicesResponse> paginatedList;
        if (request.Filter == null)
        {
            paginatedList = await _repository.ProductServices.Where(x => x.ProductServiceType == ProductServiceType.service)
                .Where(x=>x.ProductServiceType==ProductServiceType.service)
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