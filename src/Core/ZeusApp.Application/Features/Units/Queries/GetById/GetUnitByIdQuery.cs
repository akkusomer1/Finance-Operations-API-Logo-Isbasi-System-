using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Units.Queries.GetById;
public class GetUnitByIdQuery : IRequest<Result<GetUnitByIdResponse>>
{
    public int Id { get; set; }
    public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, Result<GetUnitByIdResponse>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public GetUnitByIdQueryHandler(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetUnitByIdResponse>> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.Id);
            var getunitByIdResponse = _mapper.Map<GetUnitByIdResponse>(unit);
            return await Result<GetUnitByIdResponse>.SuccessAsync(getunitByIdResponse, 200);
        }
    }

}
