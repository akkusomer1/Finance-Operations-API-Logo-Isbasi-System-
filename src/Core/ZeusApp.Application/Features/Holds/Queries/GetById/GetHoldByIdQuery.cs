using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Holds.Queries.GetById;
public class GetHoldByIdQuery : IRequest<Result<GetHoldByIdResponse>>
{
    public int Id { get; set; }
    public class GetHoldByIdQueryHandler : IRequestHandler<GetHoldByIdQuery, Result<GetHoldByIdResponse>>
    {
        private readonly IHoldRepository _holdRepository;
        private readonly IMapper _mapper;

        public GetHoldByIdQueryHandler(IHoldRepository holdRepository, IMapper mapper)
        {
            _holdRepository = holdRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetHoldByIdResponse>> Handle(GetHoldByIdQuery request, CancellationToken cancellationToken)
        {
            var hold = await _holdRepository.GetByIdAsync(request.Id);
            var getHoldByIdResponse = _mapper.Map<GetHoldByIdResponse>(hold);
            return await Result<GetHoldByIdResponse>.SuccessAsync(getHoldByIdResponse, 200);
        }
    }
}