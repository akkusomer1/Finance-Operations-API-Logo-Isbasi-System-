using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Loans.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Loans.Commands.Delete;
public class DeleteLoanCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, Result<int>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteLoanCommandHandler(ILoanRepository loanRepository, IUnitOfWork unitOfWork)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id);

            await _loanRepository.DeleteAsync(loan);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(loan.Id,200);
        }
    }
}
