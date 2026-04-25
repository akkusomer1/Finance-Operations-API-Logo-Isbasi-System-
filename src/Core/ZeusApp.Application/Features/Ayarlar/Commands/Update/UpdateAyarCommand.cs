using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Ayarlar.Commands.Update;

public class UpdateAyarCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public string SistemAdi { get; set; }
    public string Degeri { get; set; }
    public EntityStatus? Durum { get; set; }
    public int? Sira { get; set; }

    public class UpdateAyarCommandHandler : IRequestHandler<UpdateAyarCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAyarRepository _ayarRepository;

        public UpdateAyarCommandHandler(IAyarRepository ayarRepository, IUnitOfWork unitOfWork)
        {
            _ayarRepository = ayarRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateAyarCommand command, CancellationToken cancellationToken)
        {
            var ayar = await _ayarRepository.GetByIdAsync(command.Id);

            if (ayar == null)
            {
                return await Result<int>.FailAsync(404);
            }

            ayar.Adi = command.Adi ?? ayar.Adi;
            ayar.SistemAdi = command.SistemAdi ?? ayar.SistemAdi;
            ayar.Degeri = command.Degeri ?? ayar.Degeri;
            ayar.Status = command.Durum ?? ayar.Status;
            ayar.DisplayOrder = command.Sira ?? ayar.DisplayOrder;

            await _ayarRepository.UpdateAsync(ayar);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(ayar.Id);
        }
    }
}
