using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.GeneralBanks.Queries.GetById;
public class GetGeneralBankByIdQuery : IRequest<Result<GetGeneralBankByIdResponse>>
{
    public int Id { get; set; }
    public class GetGeneralBankByIdQueryHandler : IRequestHandler<GetGeneralBankByIdQuery, Result<GetGeneralBankByIdResponse>>
    {
        private readonly IGeneralBankRepository _generalBankRepository;
        private readonly IMapper _mapper;

        public GetGeneralBankByIdQueryHandler(IGeneralBankRepository generalBankRepository, IMapper mapper)
        {
            _generalBankRepository = generalBankRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetGeneralBankByIdResponse>> Handle(GetGeneralBankByIdQuery request, CancellationToken cancellationToken)
        {
            var generalBank = await _generalBankRepository.GetByIdAsync(request.Id);
            var getGeneralBankByIdResponse = _mapper.Map<GetGeneralBankByIdResponse>(generalBank);
            return await Result<GetGeneralBankByIdResponse>.SuccessAsync(getGeneralBankByIdResponse, 200);
        }
    }
}
