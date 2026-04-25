using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.GeneralBanks.Queries.GetAllPaged;
public class GetAllGeneralBanksQuery : IRequest<Result<List<GetAllGeneralBanksResponse>>>
{
}
public class GetAllGeneralBanksQueryHandler : IRequestHandler<GetAllGeneralBanksQuery, Result<List<GetAllGeneralBanksResponse>>>
{
    private readonly IGeneralBankRepository _generalBankRepository;
    private readonly IMapper _mapper;

    public GetAllGeneralBanksQueryHandler(IGeneralBankRepository generalBankRepository, IMapper mapper)
    {
        _generalBankRepository = generalBankRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllGeneralBanksResponse>>> Handle(GetAllGeneralBanksQuery request, CancellationToken cancellationToken)
    {
        var generalBanks = await _generalBankRepository.GetListAsync();
        var generalBanksResponses = _mapper.Map<List<GetAllGeneralBanksResponse>>(generalBanks);
        return Result<List<GetAllGeneralBanksResponse>>.Success(generalBanksResponses, 200);
    }
}