using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Ayarlar.Commands.Delete;

public class DeleteAyarCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public class DeleteAyarCommandHandler : IRequestHandler<DeleteAyarCommand, Result<int>>
    {
        private readonly IAyarRepository _ayarRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAyarCommandHandler(IAyarRepository ayarRepository, IUnitOfWork unitOfWork)
        {
            _ayarRepository = ayarRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteAyarCommand command, CancellationToken cancellationToken)
        {
            var ayar = await _ayarRepository.GetByIdAsync(command.Id);

            await _ayarRepository.DeleteAsync(ayar);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(ayar.Id);
        }
    }
}
