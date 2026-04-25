using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Exceptions;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Ayarlar.Commands.Update;

public class UpdateAyarImageCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public byte[] Image { get; set; }

    public class UpdateAyarImageCommandHandler : IRequestHandler<UpdateAyarImageCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAyarRepository _ayarRepository;

        public UpdateAyarImageCommandHandler(IAyarRepository ayarRepository, IUnitOfWork unitOfWork)
        {
            _ayarRepository = ayarRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateAyarImageCommand command, CancellationToken cancellationToken)
        {
            var ayar = await _ayarRepository.GetByIdAsync(command.Id);

            if (ayar == null)
            {
                throw new ApiException($"Ayar bulunamadi.");
            }

            ayar.Image = command.Image;

            await _ayarRepository.UpdateAsync(ayar);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(ayar.Id);
        }
    }
}
