using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Banks.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.BankAccounts.Commands.Create;
public class CreateBankAccountCommand : IRequest<Result<int>>
{
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public CurrencyType? Currency { get; set; }
    public decimal OpeningBalance { get; set; }
    public DateTime OpeningBalanceDate { get; set; }
    public decimal Balance { get; set; }
    public bool CreditCard { get; set; }
    public int GeneralBankId { get; set; }
}
public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, Result<int>>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IMapper _mapper;

    private IUnitOfWork _unitOfWork { get; set; }

    public CreateBankAccountCommandHandler(IBankAccountRepository bankAccountRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _bankAccountRepository = bankAccountRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
    {
        var bankAccount  = _mapper.Map<BankAccount>(request);

        await _bankAccountRepository.InsertAsync(bankAccount);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(bankAccount.Id, 200);
    }
}
