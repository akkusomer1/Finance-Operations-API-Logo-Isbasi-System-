using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Ayarlar.Commands.Create;

public partial class CreateAyarCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public string SistemAdi { get; set; }
    public string Degeri { get; set; }
    public int Durum { get; set; }
    public int? Sira { get; set; }
}

public class CreateAyarCommandHandler : IRequestHandler<CreateAyarCommand, Result<int>>
{
    private readonly IAyarRepository _ayarRepository;
    private readonly IMapper _mapper;

    private IUnitOfWork _unitOfWork { get; set; }

    public CreateAyarCommandHandler(IAyarRepository ayarRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _ayarRepository = ayarRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateAyarCommand request, CancellationToken cancellationToken)
    {
        var ayar = _mapper.Map<Ayar>(request);

        await _ayarRepository.InsertAsync(ayar);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(ayar.Id);
    }
}
