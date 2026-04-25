using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.ProductBrands.Queries.GetById;


public class GetProductBrandByIdQuery : IRequest<Result<GetProductBrandByIdResponse>>
{
    public int Id { get; set; }

}
public class GetProductBrandByIdQueryHandler : IRequestHandler<GetProductBrandByIdQuery, Result<GetProductBrandByIdResponse>>
{
    private readonly IProductBrandRepository _ProductBrandRepository;
    private readonly IMapper _mapper;

    public GetProductBrandByIdQueryHandler(IProductBrandRepository ProductBrandRepository, IMapper mapper)
    {
        _ProductBrandRepository = ProductBrandRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetProductBrandByIdResponse>> Handle(GetProductBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var ProductBrand = await _ProductBrandRepository.GetByIdAsync(request.Id);
        var getProductBrandByIdResponse = _mapper.Map<GetProductBrandByIdResponse>(ProductBrand);
        return await Result<GetProductBrandByIdResponse>.SuccessAsync(getProductBrandByIdResponse, 200);
    }
}

