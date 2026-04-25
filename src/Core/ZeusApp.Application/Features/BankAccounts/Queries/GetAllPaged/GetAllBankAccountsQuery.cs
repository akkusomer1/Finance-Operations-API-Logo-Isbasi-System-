using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Features.Cases.Queries.GetAllPaged;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.BankAccounts.Queries.GetAllPaged;
public class GetAllBankAccountsQuery : IRequest<Result<List<GetAllBankAccountsResponse>>>
{
}
public class GetAllBankAccountsQueryHandler : IRequestHandler<GetAllBankAccountsQuery, Result<List<GetAllBankAccountsResponse>>>
{
    private readonly IBankAccountRepository  _bankAccountRepository;

    public GetAllBankAccountsQueryHandler(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<Result<List<GetAllBankAccountsResponse>>> Handle(GetAllBankAccountsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<BankAccount, GetAllBankAccountsResponse>> expression = e => new GetAllBankAccountsResponse
        {
            Id = e.Id,
            AccountName = e.AccountName,
            AccountNumber = e.AccountNumber,
            IBAN = e.IBAN,
            Currency = e.Currency,
            OpeningBalance = e.OpeningBalance,
            Balance = e.Balance,
            Status = e.Status == EntityStatus.Active ? "Aktif" : "Aktif değil"
        };
        var cases = await _bankAccountRepository.BankAccounts.Select(expression).ToListAsync();
        return Result<List<GetAllBankAccountsResponse>>.Success(cases, 200);
    }
}
