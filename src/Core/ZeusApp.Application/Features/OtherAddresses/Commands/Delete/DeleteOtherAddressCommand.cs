using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.OtherAddresses.Commands.Delete;
public class DeleteOtherAddressCommand:IRequest<Result<int>>
{
    public int Id { get; set; }

    // ReSharper disable once UnusedMember.Global
    public class DeleteOtherAddressCommandHandler : IRequestHandler<DeleteOtherAddressCommand, Result<int>>
    {
        private readonly IOtherAddressRepository _otherAddressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOtherAddressCommandHandler(IOtherAddressRepository otherAddressRepository, IUnitOfWork unitOfWork)
        {
            _otherAddressRepository = otherAddressRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteOtherAddressCommand command, CancellationToken cancellationToken)
        {
            var otherAddress = await _otherAddressRepository.GetByIdAsync(command.Id);

            await _otherAddressRepository.DeleteAsync(otherAddress);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(otherAddress.Id, 200);
        }

    }
}
