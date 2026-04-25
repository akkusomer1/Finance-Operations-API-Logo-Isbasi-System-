using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.GeneralBanks.Commands.Delete;
public class DeleteGeneralBankCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

}
public class DeleteGeneralBankCommandHandler : IRequestHandler<DeleteGeneralBankCommand, Result<int>>
{
    private readonly IGeneralBankRepository _generalBankRepository;
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGeneralBankCommandHandler(IGeneralBankRepository generalBankRepository, IUnitOfWork unitOfWork, IBankAccountRepository bankAccountRepository)
    {
        _generalBankRepository = generalBankRepository;
        _unitOfWork = unitOfWork;
        _bankAccountRepository = bankAccountRepository;
    }
    public async Task<Result<int>> Handle(DeleteGeneralBankCommand request, CancellationToken cancellationToken)
    {
        var generalBank = await _generalBankRepository.GetByIdAsync(request.Id);

        if (generalBank == null)
            throw new KeyNotFoundException("Böyle Bir Banka Bulunamadı");

        await _generalBankRepository.DeleteAsync(generalBank);
        generalBank.BankAccounts.ToList().ForEach(async z =>
        {
            await _bankAccountRepository.DeleteAsync(z);
        });
        await _unitOfWork.Commit(cancellationToken);

        return Result<int>.Success(200);
    }
}