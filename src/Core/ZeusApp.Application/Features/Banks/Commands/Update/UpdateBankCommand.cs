using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Banks.Commands.Update;

public class UpdateBankCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public int CustomerSupplierId { get; set; } 

    [Display(Name = "Banka Adı")]
    public string BankName { get; set; }

    [Display(Name = "Şube Adı")]
    public string BranchName { get; set; }

    [Display(Name = "Şube Kodu")]
    public string BranchCode { get; set; }

    [Display(Name = "Hesap No *")]
    public string AccountNumber { get; set; }

    [Display(Name = "Döviz")]
    public CurrencyType? Currency { get; set; }

    [Display(Name = "IBAN *")]
    public string IBAN { get; set; }

    public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankRepository _bankRepository;

        public UpdateBankCommandHandler(IBankRepository bankRepository, IUnitOfWork unitOfWork)
        {
            _bankRepository = bankRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateBankCommand command, CancellationToken cancellationToken)
        {
            var bank = await _bankRepository.GetByIdAsync(command.Id);

            if (bank == null)
            {
                return await Result<int>.FailAsync(404);
            }

            await _bankRepository.UpdateAsync(bank);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(bank.Id, 200);
        }
    }
}
