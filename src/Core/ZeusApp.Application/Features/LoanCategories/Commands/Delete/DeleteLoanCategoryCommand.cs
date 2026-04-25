using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.LoanCategories.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.LoanCategories.Commands.Delete;
public class DeleteLoanCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public class DeleteLoanCategoryCommandHandler : IRequestHandler<DeleteLoanCategoryCommand, Result<int>>
    {
        private readonly ILoanCategoryRepository _loanCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteLoanCategoryCommandHandler(ILoanCategoryRepository loanCategoryRepository, IUnitOfWork unitOfWork)
        {
            _loanCategoryRepository = loanCategoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(DeleteLoanCategoryCommand request, CancellationToken cancellationToken)
        {
            var loanCategory = await _loanCategoryRepository.GetByIdAsync(request.Id);

            await _loanCategoryRepository.DeleteAsync(loanCategory);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(loanCategory.Id,200);
        }
    }
}
