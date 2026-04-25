using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetAllPaged;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.StockCategories.Queries.GetAllPaged;
public class GetAllStockCategoriesQuery : IRequest<Result<List<GetAllStockCategoriesResponse>>>
{
}
public class GetAllStockCategoriesQueryHandler : IRequestHandler<GetAllStockCategoriesQuery, Result<List<GetAllStockCategoriesResponse>>>
{
    private readonly IStockCategoryRepository _stockCategoryRepository;
    private readonly IMapper _mapper;

    public GetAllStockCategoriesQueryHandler(IStockCategoryRepository stockCategoryRepository, IMapper mapper)
    {
        _stockCategoryRepository = stockCategoryRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<GetAllStockCategoriesResponse>>> Handle(GetAllStockCategoriesQuery request, CancellationToken cancellationToken)
    {
        var stockCategories = await _stockCategoryRepository.GetListAsync();
        var stockCategoriesResponse = _mapper.Map<List<GetAllStockCategoriesResponse>>(stockCategories);
        return Result<List<GetAllStockCategoriesResponse>>.Success(stockCategoriesResponse, 200);
    }
}