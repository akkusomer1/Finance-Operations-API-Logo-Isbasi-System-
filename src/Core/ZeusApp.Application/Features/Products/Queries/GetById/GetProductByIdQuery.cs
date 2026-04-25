using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Products.Queries.GetById;
public class GetProductByIdQuery:IRequest<Result<GetProductByIdResponse>>
{
    public int Id { get; set; }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
{
    private readonly IProductServiceRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductServiceRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.ProductServices.Where(x => x.ProductServiceType == ProductServiceType.product)
            .SingleOrDefaultAsync(x => x.Id == query.Id);
    
        var mappedProduct = _mapper.Map<GetProductByIdResponse>(product);

        return await Result<GetProductByIdResponse>.SuccessAsync(mappedProduct, 200);
    }
}
