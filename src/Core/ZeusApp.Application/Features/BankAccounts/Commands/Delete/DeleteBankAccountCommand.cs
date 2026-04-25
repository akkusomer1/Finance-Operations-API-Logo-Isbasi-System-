using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.Banks.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZeusApp.Application.Features.BankAccounts.Commands.Delete;
public class DeleteBankAccountCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, Result<int>>
    {
        private readonly IBankAccountRepository  _bankAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBankAccountCommandHandler(IBankAccountRepository bankAccountRepository, IUnitOfWork unitOfWork)
        {
            _bankAccountRepository = bankAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _bankAccountRepository.GetByIdAsync(request.Id);

            await _bankAccountRepository.DeleteAsync(bankAccount);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(bankAccount.Id, 200);
        }
    }
}
