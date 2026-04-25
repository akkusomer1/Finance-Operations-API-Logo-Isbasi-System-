using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.ProductServiceCategories.Queries.GetAllPaged;
public class GetAllProductServiceCategoriesQuery : IRequest<Result<List<GetAllProductServiceCategoriesResponse>>>
{
    public ProductServiceType ProductServiceType { get; set; }
}

public class GetAllProductCategoriesQueryHandler : IRequestHandler<GetAllProductServiceCategoriesQuery, Result<List<GetAllProductServiceCategoriesResponse>>>
{
    private readonly IProductServiceCategoryRepository _productCategoryRepository;
    private readonly IMapper _mapper;


    public GetAllProductCategoriesQueryHandler(IProductServiceCategoryRepository productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = productCategoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllProductServiceCategoriesResponse>>> Handle(GetAllProductServiceCategoriesQuery request, CancellationToken cancellationToken)
    {

        if (request.ProductServiceType == ProductServiceType.service)
        {
            var productCategories = await _productCategoryRepository.ProductServiceCategories.Where(x => x.ProductServiceType == ProductServiceType.product).ToListAsync();
            var productCategoriesResponse1 = _mapper.Map<List<GetAllProductServiceCategoriesResponse>>(productCategories);
        }

        var serviceCategories = await _productCategoryRepository.ProductServiceCategories.Where(x => x.ProductServiceType == ProductServiceType.service).ToListAsync();
        var serviceCategoriesResponse = _mapper.Map<List<GetAllProductServiceCategoriesResponse>>(serviceCategories);
        return Result<List<GetAllProductServiceCategoriesResponse>>.Success(serviceCategoriesResponse, 200);
    }
}