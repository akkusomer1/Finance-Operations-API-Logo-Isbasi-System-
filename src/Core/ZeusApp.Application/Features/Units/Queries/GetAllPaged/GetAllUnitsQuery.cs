using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.StockCategories.Queries.GetAllPaged;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Units.Queries.GetAllPaged;
public class GetAllUnitsQuery : IRequest<Result<List<GetAllUnitsResponse>>>
{
}
public class GetAllUnitsQueryHadnler : IRequestHandler<GetAllUnitsQuery, Result<List<GetAllUnitsResponse>>>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public GetAllUnitsQueryHadnler(IUnitRepository unitRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllUnitsResponse>>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        var units = await _unitRepository.GetListAsync();
        var unitsResponses = _mapper.Map<List<GetAllUnitsResponse>>(units);
        return Result<List<GetAllUnitsResponse>>.Success(unitsResponses, 200);
    }
}
