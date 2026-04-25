using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.StockCategories.Queries.GetById;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Cases.Queries.GetById;
public class GetCaseByIdQuery : IRequest<Result<GetCaseByIdResponse>>
{
    public int Id { get; set; }
    public class GetCaseByIdQueryHandler : IRequestHandler<GetCaseByIdQuery, Result<GetCaseByIdResponse>>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;

        public GetCaseByIdQueryHandler(ICaseRepository caseRepository, IMapper mapper)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetCaseByIdResponse>> Handle(GetCaseByIdQuery request, CancellationToken cancellationToken)
        {
            var @case = await _caseRepository.GetByIdAsync(request.Id);
            var caseByIdResponse = _mapper.Map<GetCaseByIdResponse>(@case);
            return await Result<GetCaseByIdResponse>.SuccessAsync(caseByIdResponse, 200);
        }
    }
}
