using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;


namespace ZeusApp.Application.Features.ProductBrands.Queries.GetAllPaged;
public class GetAllProductBrandsQuery : IRequest<Result<List<GetAllProductBrandsResponse>>>
{

}
public class GetAllProductBrandsQueryHandler : IRequestHandler<GetAllProductBrandsQuery, Result<List<GetAllProductBrandsResponse>>>
{
    private readonly IProductBrandRepository _productBrandRepository;
    private readonly IMapper _mapper;

    public GetAllProductBrandsQueryHandler(IProductBrandRepository ProductBrandRepository, IMapper mapper)
    {
        _productBrandRepository = ProductBrandRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<GetAllProductBrandsResponse>>> Handle(GetAllProductBrandsQuery request, CancellationToken cancellationToken)
    {
        var ProductBrands = await _productBrandRepository.GetListAsync();
        var ProductBrandsResponse = _mapper.Map<List<GetAllProductBrandsResponse>>(ProductBrands);
        return Result<List<GetAllProductBrandsResponse>>.Success(ProductBrandsResponse, 200);
    }
}