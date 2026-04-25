using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.Banks.Commands.Update;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZeusApp.Application.Features.BankAccounts.Commands.Update;
public class UpdateBankAccountCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public CurrencyType? Currency { get; set; }
    public decimal OpeningBalance { get; set; }
    public DateTime OpeningBalanceDate { get; set; }
    public decimal Balance { get; set; }
    public bool CreditCard { get; set; }
    public int GeneralBankId { get; set; }
    public class UpdateBankAccountCommandHandler : IRequestHandler<UpdateBankAccountCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountRepository  _bankAccountRepository;

        public UpdateBankAccountCommandHandler(IUnitOfWork unitOfWork, IBankAccountRepository bankAccountRepository)
        {
            _unitOfWork = unitOfWork;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<Result<int>> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _bankAccountRepository.GetByIdAsync(request.Id);

            if (bankAccount == null)
            {
                return await Result<int>.FailAsync(404);
            }
            bankAccount.AccountName = request.AccountName;
            bankAccount.AccountNumber = request.AccountNumber;
            bankAccount.IBAN = request.IBAN;
            bankAccount.Currency = request.Currency;
            bankAccount.OpeningBalance = request.OpeningBalance;
            bankAccount.OpeningBalanceDate = request.OpeningBalanceDate;
            bankAccount.Balance = request.Balance;
            bankAccount.CreditCard = request.CreditCard;
            bankAccount.GeneralBankId = request.GeneralBankId;
            await _bankAccountRepository.UpdateAsync(bankAccount);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(bankAccount.Id, 200);
        }
    }
}
