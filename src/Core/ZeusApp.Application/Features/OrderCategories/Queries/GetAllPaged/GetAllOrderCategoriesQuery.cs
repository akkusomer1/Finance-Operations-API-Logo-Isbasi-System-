using System.Linq.Expressions;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.OrderCategories.Queries.GetAllPaged;
public class GetAllOrderCategoriesQuery : IRequest<Result<List<GetAllOrderCategoriesResponse>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<OrderCategory, bool>> Filter { get; set; }


    public GetAllOrderCategoriesQuery(int pageNumber, int pageSize, Expression<Func<OrderCategory, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}
public class GetAllOrderCategoriesQueryHandler : IRequestHandler<GetAllOrderCategoriesQuery, Result<List<GetAllOrderCategoriesResponse>>>
{
    private readonly IOrderCategoryRepository _orderCategoryRepository;

    public GetAllOrderCategoriesQueryHandler(IOrderCategoryRepository orderCategoryRepository)
    {
        _orderCategoryRepository = orderCategoryRepository;
    }
    public async Task<Result<List<GetAllOrderCategoriesResponse>>> Handle(GetAllOrderCategoriesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<OrderCategory, GetAllOrderCategoriesResponse>> expression = e => new GetAllOrderCategoriesResponse
        {
            Id = e.Id,
            Name = e.Name,
        };

        var orderCategory = await _orderCategoryRepository.OrderCategories.Select(expression).ToListAsync();
        return Result<List<GetAllOrderCategoriesResponse>>.Success(orderCategory,200);
    }
}

