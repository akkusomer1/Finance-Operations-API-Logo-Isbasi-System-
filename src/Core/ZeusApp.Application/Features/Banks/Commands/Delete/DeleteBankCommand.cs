using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Banks.Commands.Delete;

public class DeleteBankCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand, Result<int>>
    {
        private readonly IBankRepository _bankRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBankCommandHandler(IBankRepository bankRepository, IUnitOfWork unitOfWork)
        {
            _bankRepository = bankRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteBankCommand command, CancellationToken cancellationToken)
        {
            var bank = await _bankRepository.GetByIdAsync(command.Id);

            await _bankRepository.DeleteAsync(bank);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(bank.Id, 200);
        }
    }
}