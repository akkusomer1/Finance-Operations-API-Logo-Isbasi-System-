
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Ayarlar.Queries.GetById;

public class GetAyarByIdQuery : IRequest<Result<GetAyarByIdResponse>>
{
    public int Id { get; set; }

    public class GetAyarByIdQueryHandler : IRequestHandler<GetAyarByIdQuery, Result<GetAyarByIdResponse>>
    {
        private readonly IAyarRepository _ayarRepository;
        private readonly IMapper _mapper;

        public GetAyarByIdQueryHandler(IAyarRepository ayarRepository, IMapper mapper)
        {
            _ayarRepository = ayarRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetAyarByIdResponse>> Handle(GetAyarByIdQuery query, CancellationToken cancellationToken)
        {
            var ayar = await _ayarRepository.GetByIdAsync(query.Id);
            var mappedAyar = _mapper.Map<GetAyarByIdResponse>(ayar);

            return await Result<GetAyarByIdResponse>.SuccessAsync(mappedAyar, 200);
        }
    }
}
