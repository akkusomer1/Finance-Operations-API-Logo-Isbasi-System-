using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Stocks.Queries.GetById;
public class GetStockByIdQuery : IRequest<Result<GetStockByIdResponse>>
{
    public int Id { get; set; }
}

public class GetStockByIdQueryHandler : IRequestHandler<GetStockByIdQuery, Result<GetStockByIdResponse>>
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public GetStockByIdQueryHandler(IStockRepository stockRepository, IMapper mapper)
    {
        _stockRepository = stockRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetStockByIdResponse>> Handle(GetStockByIdQuery query, CancellationToken cancellationToken)
    {
        var stock = await _stockRepository.GetByIdAsync(query.Id);
       
        var mappedStock = _mapper.Map<GetStockByIdResponse>(stock);

        return await Result<GetStockByIdResponse>.SuccessAsync(mappedStock, 200);
    }
}

