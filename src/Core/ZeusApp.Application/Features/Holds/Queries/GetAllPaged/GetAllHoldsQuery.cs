using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Holds.Queries.GetAllPaged;
public class GetAllHoldsQuery : IRequest<Result<List<GetAllHoldsResponse>>>
{

}
public class GetAllHoldsHandler : IRequestHandler<GetAllHoldsQuery, Result<List<GetAllHoldsResponse>>>
{
    private readonly IHoldRepository _holdRepository;
    private readonly IMapper _mapper;

    public GetAllHoldsHandler(IHoldRepository holdRepository, IMapper mapper)
    {
        _holdRepository = holdRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<GetAllHoldsResponse>>> Handle(GetAllHoldsQuery request, CancellationToken cancellationToken)
    {
        var holds = await _holdRepository.GetListAsync();
        var holdsResponse = _mapper.Map<List<GetAllHoldsResponse>>(holds);
        return Result<List<GetAllHoldsResponse>>.Success(holdsResponse, 200);
    }
}